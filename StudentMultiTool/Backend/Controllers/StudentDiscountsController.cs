using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.StudentDiscounts;
using StudentMultiTool.Backend.Services.UserManagement;

namespace StudentMultiTool.Backend.Controllers
{
    [Route("api/" + "studentdiscounts")]
    [ApiController]
    public class StudentDiscountsController : ControllerBase
    {
        private readonly ILogger<StudentDiscounts> _logger;
        public StudentDiscountsController(ILogger<StudentDiscounts> logger)
        {
            _logger = logger;
        }

        // validateInput method returns an array of IEnumerable with the valid or invalid
        // values for the user's input
        [HttpGet("verification/{username}")]
        public IActionResult verification(string username)
        {
            InputValidation userValidation = new InputValidation();
            if (userValidation.usernameExists(username))
            {

                return Ok("valid");
            }
            else
            {
                return BadRequest("error in api");
            }
            
        }
    }
}
