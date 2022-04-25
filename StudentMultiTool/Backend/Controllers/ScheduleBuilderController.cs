using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleBuilder;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Nodes;

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
            // TODO: convert WL's to logs
            Console.WriteLine("ScheduleController.GetList with args");
            Console.WriteLine("\tusername: \"" + username + "\"");
            // Get user hash so we know who to get schedules for
            string? userHash = null;
            Console.WriteLine("Getting user hash for user \"" + username + "\"");
            ScheduleListBuilder builder = new ScheduleListBuilder();
            userHash = builder.GetUserHash(username);
            if (userHash == null)
            {
                Console.WriteLine("userHash was null");
                return Enumerable.Empty<Schedule>();
            }

            // Get all schedules the user owns (and/or can collaborate on)
            IEnumerable<Schedule> list = builder.GetAllSchedulesForUser(userHash).Result;
            Console.WriteLine("Request finished");
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
        //public List<ScheduleItemDTO> GetSchedule(string user, int scheduleId)
        //public JsonResult GetSchedule(string user, int scheduleId)
        //public ActionResult GetSchedule(string user, int scheduleId)
        {
            Console.WriteLine("ScheduleController.GetSchedule with args");
            Console.WriteLine("\tuser: \"" + user + "\"");
            Console.WriteLine("\tscheduleId: \"" + scheduleId + "\"");
            //IEnumerable<ScheduleItemDTO> items = Enumerable.Empty<ScheduleItemDTO>();
            //List<ScheduleItemDTO> items = new List<ScheduleItemDTO>();
            IEnumerable<ScheduleItemDTO> items = new List<ScheduleItemDTO>();

            ScheduleManager manager = new ScheduleManager();
            Schedule? schedule = manager.SelectScheduleWithItems(scheduleId);
            if (schedule != null)
            {
                foreach (ScheduleItem si in schedule.Items)
                {
                    Console.WriteLine(si.Title);
                    ScheduleItemDTO temp = new ScheduleItemDTO(si);
                    temp.ScheduleId = scheduleId;
                    items.Append(temp);
                }
            }
            //else
            //{
            //    return StatusCode(500, "Schedule was null");
            //}
            Console.WriteLine("Request finished with " + schedule.Items.Count + " items");
            return items;
            //return new JsonResult(items);
        }

        // Create a new schedule for a user.
        // Returns the status of the operation.
        [HttpPost]
        [HttpPost("newschedule/{user}/{title}")]
        public string NewSchedule(string user, string title)
        {
            // TODO: check that the user is authenticated
            // TODO: convert WL's to logs
            Console.WriteLine("ScheduleController.NewSchedule with args");
            Console.WriteLine("\tuser: \"" + user + "\"");
            Console.WriteLine("\ttitle: \"" + title + "\"");
            int rowsAffected = 0;
            string? userHash = null;

            Console.WriteLine("Getting user hash for user \"" + user + "\"");
            ScheduleListBuilder builder = new ScheduleListBuilder();
            userHash = builder.GetUserHash(user);
            if (userHash == null)
            {
                return "Could not get userHash";
            }

            // Add schedule to DB
            Console.WriteLine("Add schedule to db");
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
                Console.WriteLine("Add the owner as a collaborator to the db");
                rowsAffected = manager.InsertCollaborator((int) newId, userHash, true, true);
            }

            if (rowsAffected > 0 )
            {
                return "Success";
            }
            // TODO: make a better default return value
            return "Oops";
        }

        [HttpPost("saveSchedule")]
        //public ActionResult SaveSchedule(string data)
        public ActionResult SaveSchedule(ScheduleDTO data)
        {
            Console.WriteLine("ScheduleController.SaveSchedule with data:");
            Console.WriteLine(data.ScheduleId);
            foreach (ScheduleItemDTO si in data.Items)
            {
                Console.WriteLine(si);
            }
            Console.WriteLine(data.Modified);
            string nullOrEmptyResult = "Request data was null or empty";
            //JsonResult input = new JsonResult();
            // Make sure the data isn't null or empty
            //if (!string.IsNullOrEmpty(data))
            if (data != null)
            {
                // Unpack the data
                //JsonNode? input = null;
                //JsonArray? items = null;
                //int? scheduleId = null;
                //try
                //{
                //    // Unpack the data. If it's null, the request was probably malformed
                //    //Schedule current = JsonSerializer.Deserialize<Schedule>(data);
                //    //input = JsonNode.Parse(data);
                //    //if (input == null)
                //    //{
                //    //    return BadRequest(nullOrEmptyResult);
                //    //}

                //    // Unpack the scheduleId. If it's not there, we can't really do anything.
                //    //scheduleId = (int) input!["scheduleId"]!;
                //    if (scheduleId == null)
                //    {
                //        return BadRequest("ScheduleId was null");
                //    }

                //    // Unpack the items. If it's empty, it won't be null. If it's null, the request
                //    // was probably malformed.
                //    //items = (JsonArray) input!["items"]!;
                //    if (items == null)
                //    {
                //        return BadRequest("Items was null");
                //    }
                //}
                //catch (ArgumentNullException ex)
                //{
                //    return BadRequest("Could not save due to null argument");
                //}
                //catch (JsonException ex)
                //{
                //    return BadRequest("JsonException");
                //}

                // If unpacking was successful, continue
                //if (data.ScheduleId != null)
                //{
                // Get the file path to write the schedule items to
                ScheduleManager manager = new ScheduleManager();
                Schedule? schedule = manager.SelectScheduleWithoutItems(data.ScheduleId);
                if (schedule == null)
                {
                    // TODO: update modified datetime. add a schedulemanager method for it
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
                //ScheduleFileAccessor accessor = new ScheduleFileAccessor();
                //string result = accessor.WriteScheduleItems(schedule);
                string result = manager.SaveSchedule(ref schedule);
                Console.WriteLine(result);
                // Done!
                if (result.Equals(ScheduleFileAccessor.Success))
                {
                    return Ok("Successfully saved schedule");
                }
                else
                {
                    return StatusCode(500, "Could not save schedule to file");
                }
                //}
            }
            return BadRequest(nullOrEmptyResult);
        }

        [HttpPost]
        [HttpPost("{user}/{scheduleId}/{newItem}")]
        //public string CreateItem(string user, int scheduleId, JsonObject newItem)
        public string CreateItem(ScheduleItemDTO newItem)
        {
            Console.WriteLine("ScheduleController.CreateItem with args");
            Console.WriteLine("\tnewItem: \"" + newItem + "\"");
            if (newItem != null)
            {
                string user = newItem.Creator;
                int scheduleId = newItem.ScheduleId;
                Console.WriteLine("\tuser: \"" + user + "\"");
                Console.WriteLine("\tscheduleId: \"" + scheduleId + "\"");
                // Load the schedule
                ScheduleManager manager = new ScheduleManager();
                Schedule? schedule = manager.SelectScheduleWithItems(scheduleId);
                if (schedule != null)
                {
                    ScheduleItem item = new ScheduleItem(newItem);

                    // add the item to the schedule
                    manager.CreateScheduleItem(ref schedule, ref item);
                }
                // TODO: return error message if the item couldn't be added
            }
            // TODO: return error message if null
            // TODO: make a better default return value
            Console.WriteLine("Returning");
            return "Oops";
        }

        [HttpPost]
        [HttpPost("updateItem/{user}/{scheduleId}")]
        //public string UpdateItem(string user, int scheduleId)
        public string UpdateItem(ScheduleItemDTO updatedItem)
        {
            Console.WriteLine("ScheduleController.UpdateItem with args");
            Console.WriteLine("\tnewItem: \"" + updatedItem + "\"");
            if (updatedItem != null)
            {
                string user = updatedItem.Creator;
                int scheduleId = updatedItem.ScheduleId;
                Console.WriteLine("\tuser: \"" + user + "\"");
                Console.WriteLine("\tscheduleId: \"" + scheduleId + "\"");
                // Load the schedule
                ScheduleManager manager = new ScheduleManager();
                Schedule? schedule = manager.SelectScheduleWithItems(scheduleId);
                if (schedule != null)
                {
                    ScheduleItem item = new ScheduleItem(updatedItem);

                    // update the item
                    manager.UpdateScheduleItem(ref schedule, ref item);
                }
                // TODO: return error message if the item couldn't be updated
            }
            // TODO: return error message if null
            // TODO: make a better default return value
            Console.WriteLine("Returning");
            return "Oops";
        }

        [HttpDelete]
        [HttpDelete("deleteItem/{user}/{scheduleId}/{deleteableItemId}")]
        //public string DeleteItem(string user, int scheduleId, int deleteableItemId)
        public string DeleteItem(ScheduleItemDTO deleteableItem)
        {
            Console.WriteLine("ScheduleController.DeleteItem with args");
            Console.WriteLine("\tnewItem: \"" + deleteableItem + "\"");
            if (deleteableItem != null)
            {
                string user = deleteableItem.Creator;
                int scheduleId = deleteableItem.ScheduleId;
                Console.WriteLine("\tuser: \"" + user + "\"");
                Console.WriteLine("\tscheduleId: \"" + scheduleId + "\"");
                // Load the schedule
                ScheduleManager manager = new ScheduleManager();
                Schedule? schedule = manager.SelectScheduleWithItems(scheduleId);
                if (schedule != null)
                {
                    ScheduleItem item = new ScheduleItem(deleteableItem);

                    // delete the item from the schedule
                    manager.DeleteScheduleItem(ref schedule, item);
                }
                // TODO: return error message if the item couldn't be deleted
            }
            // TODO: return error message if null
            // TODO: make a better default return value
            Console.WriteLine("Returning");
            return "Oops";
        }
    }
}
