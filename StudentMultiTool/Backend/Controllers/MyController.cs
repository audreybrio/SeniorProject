using Microsoft.AspNetCore.Mvc;

namespace StudentMultiTool.Backend.Controllers
{
    // Contains debug actions
    [ApiController]
    [Route("mycontroller")]
    public class MyController : ControllerBase
    {
        [HttpGet]
        public IActionResult UtcNow()
        {
            return Ok(DateTime.UtcNow);
        }

        [HttpGet]
        public IActionResult ControllersWorking()
        {
            return Ok("Controllers are working");
        }

        [HttpGet("[action]/{text}")]
        public IActionResult Echo(string text)
        {
            return Ok(text);
        }
    }
}
