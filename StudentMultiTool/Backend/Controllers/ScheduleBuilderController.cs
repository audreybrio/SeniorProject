using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleBuilder;
using StudentMultiTool.Backend.Services.UserManagement;
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
            ScheduleManager manager = new ScheduleManager();
            IEnumerable<Schedule> result = manager.GetList(username);
            return result;
            return manager.GetList(username);
        }

        // Return a schedule and its contents based on the ID of the schedule.
        // Always returns an enumerable, but the enumerable may be empty.
        [HttpGet("getschedule/{user}/{scheduleId}")]
        public IEnumerable<ScheduleItemDTO> GetSchedule(string user, int scheduleId)
        {
            ScheduleManager manager = new ScheduleManager();
            return manager.GetSchedule(user, scheduleId);
        }

        // Create a new schedule for a user.
        // Returns the status of the operation.
        [HttpPost]
        [HttpPost("newschedule/{user}/{title}")]
        public string NewSchedule(string user, string title)
        {
            ScheduleManager manager = new ScheduleManager();
            string result = manager.NewSchedule(user, title);
            if (result.Equals(manager.Success))
            {
                return result;
            }
            return result;
        }

        [HttpPost("saveSchedule")]
        //public ActionResult SaveSchedule(string data)
        public ActionResult SaveSchedule(ScheduleDTO data)
        {
            ScheduleManager manager = new ScheduleManager();
            string result = manager.SaveSchedule(data);
            if (result.Equals(manager.Success))
            {
                return Ok(result);
            }
            return StatusCode(500, result);
        }

        [HttpPost]
        [Route("addCollaborator/{scheduleId}/{username}")]
        public IActionResult AddCollaborator(int scheduleId, string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be blank");
            }
            ScheduleManager manager = new ScheduleManager();
            string result = manager.AddCollaborator(scheduleId, username);
            if (result.Equals(manager.Success))
            {
                return Ok(result);
            }
            return StatusCode(500, result);
        }

        [HttpPost]
        [Route("updateCollaborator/{scheduleId}")]
        public IActionResult UpdateCollaborator(int scheduleId, [FromBody] CollaboratorDTO collaborator)
        {
            if (string.IsNullOrEmpty(collaborator.Username))
            {
                return BadRequest("Username cannot be blank");
            }
            ScheduleManager manager = new ScheduleManager();
            string result = manager.UpdateCollaborator(scheduleId, collaborator);
            if (result.Equals(manager.Success))
            {
                return Ok(result);
            }
            return StatusCode(500, result);
        }

        [HttpPost]
        [Route("deleteCollaborator/{scheduleId}")]
        public IActionResult DeleteCollaborator(int scheduleId, [FromBody] CollaboratorDTO collaborator)
        {
            if (string.IsNullOrEmpty(collaborator.Username))
            {
                return BadRequest("Username cannot be blank");
            }
            ScheduleManager manager = new ScheduleManager();
            string result = manager.DeleteCollaborator(scheduleId, collaborator);
            if (result.Equals(manager.Success))
            {
                return Ok(result);
            }
            return StatusCode(500, result);
        }

        [HttpGet]
        [Route("getCollaborators/{scheduleId}")]
        public IEnumerable<CollaboratorDTO> GetCollaborators(int scheduleId)
        {
            ScheduleManager manager = new ScheduleManager();
            return manager.GetCollaborators(scheduleId);
        }

        [HttpGet]
        [Route("searchUser/{username}")]
        public IActionResult SearchUser(string username)
        {
            UserManager manager = new UserManager();
            if (manager.SearchUsers(username))
            {
                return Ok();
            }
            return StatusCode(404, "user " + username + " not found");
        }
    }
}
