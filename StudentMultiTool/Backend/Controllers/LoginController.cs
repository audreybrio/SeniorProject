using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using StudentMultiTool.Backend.Services.Authentication;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/" + "login")]
    public class LoginController : Controller
    {

        [HttpPost("validate")]
        public IActionResult EmailPasscodeCheck([FromBody] DataObj credientials)
        {
            string email = credientials.creditentials[0];
            string passcode = credientials.creditentials[1];

            Authenticate authenEmail = new Authenticate();
            bool isValid = authenEmail.EmailPasscodeCheck(email, passcode);
            if (!isValid)
            {
                return NotFound();
            }
            return Ok();
      
        }

        [HttpPost("authenticate")]
        public IActionResult AuthenticateUser([FromBody] DataObj2 authen)
        {

            string username = authen.authen[0];
            string otp = authen.authen[1];

            Authenticate authenUsername = new Authenticate();
            string token = authenUsername.AuthenticateUser(username, otp);

           if (token == null)
            {
                return NotFound();
            }
            return Ok(token);


        }


        [HttpGet]
        [Route("disable/{email}")]
        public IActionResult DisableUser(string email)
        {
            Authenticate authenticate = new Authenticate();
            bool isDisabled = authenticate.DisableUser(email);
            if (!isDisabled)
            {
                return NotFound();
            }
            return Ok();
        }
    }


    public class DataObj
    {
        public List<string> creditentials { get; set; }
    }
    public class DataObj2
    {
        public List<string> authen { get; set; }
    }

}
