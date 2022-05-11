using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Services.Matching;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Controllers
{

    // Activity Profile Controller 
    [ApiController]
    [Route("api/" + "activityProfile")]
    public class ActivityProfileController : Controller
    {
        // Sends users selections to be inserted into database
        [HttpPost("update/{username}/{opt}")]
        public IActionResult ActivityProfile([FromBody] Data activities, string username, bool opt)
        {
            Activity activity = new Activity();
            bool isActivityUpdated = activity.ActivityProfile(activities.activities, username, opt);
            if (!isActivityUpdated)
            {
                return NotFound();
            }

            return Ok();
        }
    }

    // Used to get list from frontend
    public class Data
    {
        public List<string> activities { get; set; }
    }
}
