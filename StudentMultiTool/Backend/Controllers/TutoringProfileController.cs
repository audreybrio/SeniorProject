using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Services.Matching;

namespace StudentMultiTool.Backend.Controllers

{
    [ApiController]
    [Route("api/" + "tutoringProfile")]
    public class TutoringProfileController : Controller
    {

        // Individual  is 1 if want individual, 0 if want group
        // requires is 1 if requires tutroing, 0 if offering

        [HttpPost]
        [Route("update/{username}/{individual}/{requires}/{opt}")]
        public IActionResult TutoringProfile([FromBody] DataT courses, string username, bool individual, bool requires, bool opt)
        {

            bool updateTutoring;
            updateTutoring = Tutoring.TutoringProfile(courses.courses, username, individual, requires, opt);
            if (!updateTutoring)
            {
                return Ok();
            }

            return Ok();
        }


    }
    public class DataT
    {
        public List<string> courses { get; set; }
    }
}
