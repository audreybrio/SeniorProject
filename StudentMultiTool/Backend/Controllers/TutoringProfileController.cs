using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Services.Matching;

namespace StudentMultiTool.Backend.Controllers

{
    // Tutoring Profile Controller
    [ApiController]
    [Route("api/" + "tutoringProfile")]
    public class TutoringProfileController : Controller
    {
        
        // Sends users selections to be inserted into database 
        [HttpPost]
        [Route("update/{username}/{individual}/{requires}/{opt}")]
        public IActionResult TutoringProfile([FromBody] DataT courses, string username, bool individual, bool requires, bool opt)
        {

            bool updateTutoring;
            updateTutoring = Tutoring.TutoringProfile(courses.courses, username, individual, requires, opt);
            if (!updateTutoring)
            {
                return NotFound();
            }

            return Ok();
        }


    }

    // Used to get list from frontend 
    public class DataT
    {
        public List<string> courses { get; set; }
    }
}
