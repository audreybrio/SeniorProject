using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.DAL;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.Logging;
using StudentMultiTool.Backend.Services.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleComparison;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Nodes;
using UserAcc;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleBuilderController : Controller
    {
        // Return a "list" (enumerable) of schedules for a given user.
        // Only returns a list of schedules that the user is listed as a
        // collaborator for.
        // Will always return a list, but there's always the possibility
        // that the list will be empty.
        [HttpGet("getlist/{username}")]
        public IEnumerable<Schedule> GetList(string username)
        {
            // TODO: check that the user is authenticated
            // Get user hash so we know who to get schedules for
            string? userHash = null;
            ScheduleListBuilder builder = new ScheduleListBuilder();
            userHash = builder.GetUserHash(username);
            if (userHash == null)
            {
                return Enumerable.Empty<Schedule>();
            }

            // Get all schedules the user owns (and/or can collaborate on)
            IEnumerable<Schedule> list = builder.GetAllSchedulesForUser(userHash).Result;
            if (list == null)
            {
                return Enumerable.Empty<Schedule>();
            }
            return list;
        }

        // Return a schedule and its contents based on the ID of the schedule.
        // Always returns an enumerable, but the enumerable may be empty.
        [HttpGet("getschedule/{user}/{scheduleId}")]
        public IEnumerable<ScheduleItemDTO> GetSchedule(string user, int scheduleId)
        {
            List<ScheduleItemDTO> items = new List<ScheduleItemDTO>();

            // Check that the user has permission to edit the schedule
            SchedulePermissionValidator validator = new SchedulePermissionValidator();
            ScheduleListBuilder listBuilder = new ScheduleListBuilder();
            string? hash = listBuilder.GetUserHash(user);
            int isCollaborator = 0;

            // If the user's hash is null, then they probably aren't logged in
            if (!string.IsNullOrEmpty(hash))
            {
                isCollaborator = validator.IsCollaborator(hash, scheduleId);
            }

            // If the user is a collaborator on the schedule, continue
            if (isCollaborator > 0)
            {
                // Get the schedule's items
                ScheduleManager manager = new ScheduleManager();
                Schedule? schedule = manager.SelectScheduleWithItems(scheduleId);
                if (schedule != null)
                {
                    // Prep the items for data transfer
                    foreach (ScheduleItem si in schedule.Items)
                    {
                        Console.WriteLine(si.Title);
                        ScheduleItemDTO temp = new ScheduleItemDTO(si);
                        temp.ScheduleId = scheduleId;
                        items.Add(temp);
                    }

                    // Return the items
                    return items;
                }
            }
            // If something went wrong or the user wasn't a collaborator, return an empty list
            return Enumerable.Empty<ScheduleItemDTO>();
        }

        // Create a new schedule for a user.
        // Returns the status of the operation.
        [HttpPost]
        [HttpPost("newschedule/{user}/{title}")]
        public string NewSchedule(string user, string title)
        {
            // TODO: check that the user is authenticated
            int rowsAffected = 0;
            string? userHash = null;

            ScheduleListBuilder builder = new ScheduleListBuilder();
            userHash = builder.GetUserHash(user);
            if (userHash == null)
            {
                return "Could not get userHash";
            }

            // Add schedule to DB
            Schedule newSchedule = new Schedule(
               -1,
               DateTime.Now,
               DateTime.Now,
               title,
               userHash + "-" + title + ".json"
            );
            ScheduleManager manager = new ScheduleManager();
            int? newId = manager.InsertSchedule(newSchedule);

            if (newId != null)
            {
                // Add the owner as a collaborator to the DB
                rowsAffected = manager.InsertCollaborator((int) newId, userHash, true, true);
            }

            if (rowsAffected > 0)
            {
                return "Success";
            }
            
            return "Could not create new schedule";
        }

        [HttpPost("saveSchedule")]
        //public ActionResult SaveSchedule(string data)
        public ActionResult SaveSchedule(ScheduleDTO data)
        {
            string nullOrEmptyResult = "Request data was null or empty";
            // Make sure the data isn't null or empty
            if (data != null)
            {
                // Get the file path to write the schedule items to
                ScheduleManager manager = new ScheduleManager();
                Schedule? schedule = manager.SelectScheduleWithoutItems(data.ScheduleId);
                if (schedule == null)
                {
                    return StatusCode(500, "Could not find schedule with id " + data.ScheduleId);
                }

                // Unpack the ScheduleItemDTOs and place them in the schedule.
                // The ScheduleItem constructor will handle most of the work here
                foreach (ScheduleItemDTO sid in data.Items)
                {
                    ScheduleItem current = new ScheduleItem(sid);
                    schedule.AddScheduleItem(current);
                }

                // Write the schedule items to the schedule's file
                string result = manager.SaveSchedule(ref schedule);

                // Done!
                if (result.Equals(ScheduleFileAccessor.Success))
                {
                    return Ok("Successfully saved schedule");
                }
                else
                {
                    return StatusCode(500, "Could not save schedule to file");
                }

            }
            return BadRequest(nullOrEmptyResult);
        }

        [HttpPost]
        [Route("schedule/addCollaborator/{scheduleId}")]
        public IActionResult AddCollaborator(int scheduleId, [FromBody] string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be blank");
            }
            ScheduleManager manager = new ScheduleManager();
            Schedule? schedule = manager.SelectScheduleWithoutItems(scheduleId);
            if (schedule != null)
            {
                UserAccountDAO uad = new UserAccountDAO();
                UserAccount? user = uad.SelectSingle(username);
                if (user != null)
                {
                    Hasher hasher = new Hasher();
                    string userHash = hasher.HashUsername(username);
                    int rowsAffected = manager.InsertCollaborator(scheduleId, userHash, true, false);
                    if (rowsAffected < 1)
                    {
                        return StatusCode(500, "Could not add collaborator");
                    }
                    return Ok(username + " successfully added as a collaborator");
                }
                return StatusCode(404, "User " + username + "could not be found");
            }
            return StatusCode(404, "Schedule with id " + scheduleId + "could not be found");
        }

        [HttpPost]
        [Route("schedule/updateCollaborator/{scheduleId}")]
        public IActionResult UpdateCollaborator(int scheduleId, [FromBody] CollaboratorDTO collaborator)
        {
            if (string.IsNullOrEmpty(collaborator.Username))
            {
                return BadRequest("Username cannot be blank");
            }
            string username = collaborator.Username;
            ScheduleManager manager = new ScheduleManager();
            Schedule? schedule = manager.SelectScheduleWithoutItems(scheduleId);
            if (schedule != null)
            {
                UserAccountDAO uad = new UserAccountDAO();
                UserAccount? user = uad.SelectSingle(username);
                if (user != null)
                {
                    Hasher hasher = new Hasher();
                    string userHash = hasher.HashUsername(username);
                    int rowsAffected = 0;
                    rowsAffected = manager.UpdateCollaborator(scheduleId, userHash, collaborator.CanWrite, collaborator.IsOwner);
                    if (rowsAffected < 1)
                    {
                        return StatusCode(500, "Could not update collaborator " + username);
                    }
                    return Ok("Collaborator" + username + " successfully updated");
                }
                return StatusCode(404, "User " + username + "could not be found");
            }
            return StatusCode(404, "Schedule with id " + scheduleId + "could not be found");
        }

        [HttpPost]
        [Route("schedule/deleteCollaborator/{scheduleId}")]
        public IActionResult DeleteCollaborator(int scheduleId, [FromBody] CollaboratorDTO collaborator)
        {
            if (string.IsNullOrEmpty(collaborator.Username))
            {
                return BadRequest("Username cannot be blank");
            }
            string username = collaborator.Username;
            ScheduleManager manager = new ScheduleManager();
            Schedule? schedule = manager.SelectScheduleWithoutItems(scheduleId);
            if (schedule != null)
            {
                UserAccountDAO uad = new UserAccountDAO();
                UserAccount? user = uad.SelectSingle(username);
                if (user != null)
                {
                    Hasher hasher = new Hasher();
                    string userHash = hasher.HashUsername(username);
                    int rowsAffected = 0;
                    rowsAffected = manager.DeleteCollaborator(scheduleId, userHash);
                    if (rowsAffected < 1)
                    {
                        return StatusCode(500, "Could not delete collaborator " + username);
                    }
                    return Ok(username + " successfully removed as a collaborator");
                }
                return StatusCode(404, "User " + username + "could not be found");
            }
            return StatusCode(404, "Schedule with id " + scheduleId + "could not be found");
        }
    }
}
