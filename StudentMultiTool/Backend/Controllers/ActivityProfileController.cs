using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Services.Matching;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/" + "activityProfile")]
    public class ActivityProfileController : Controller
    {


        //[HttpPost("updateMain")]
        //public IActionResult HelpMe([FromBody] Data activities) {
        //    var x = Request.Body;
        //    return Ok("hello");
        //}

        [HttpPost("update/{username}/{opt}")]
        public IActionResult ActivityProfile([FromBody] Data activities, string username, bool opt)
        {

            bool isActivityUpdated = Activity.ActivityProfile(activities.activities, username, opt);
            if (!isActivityUpdated)
            {
                return Ok();
            }

            return Ok();
        }
    }

    public class Data
    {
        public List<string> activities { get; set; }
    }
}
