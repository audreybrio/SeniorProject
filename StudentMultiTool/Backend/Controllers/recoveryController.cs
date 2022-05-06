using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Controllers;
using StudentMultiTool.Backend.Models.RecoveryAccount;
using StudentMultiTool.Backend.Models.Registration;
using StudentMultiTool.Backend.Services.Authentication;
using StudentMultiTool.Backend.Services.UserManagement;
using System.Data;

namespace StudentMultiTool.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class recoveryController : Controller
    {
        private readonly ILogger<recoveryController> _logger;

        public recoveryController(ILogger<recoveryController> logger)
        {
            _logger = logger;
        }

        RecoveryDB db = new RecoveryDB();


        [HttpPost("reset")]
        public IActionResult Post(RecoveryUserEmail r)
        {
            string m = "nothing";

            if (db.sendEmailPasswordReset(r))
            {
                m = "successful";
            }
            else
            {
                m = "Error";
            }
            
            return new JsonResult(m);
        }

        [HttpGet("validate/{username}/{email}")]
        public IActionResult validateInput(string username, string email)
        {
            
            bool result = false;
            int userID  = 0;


            InputValidation inputValidation = new InputValidation();

            
            if (inputValidation.emailExists(email) && inputValidation.usernameExists(username))
            {
                result = true;
            }else
            {
                result = false;
            }

            return new JsonResult(result); 
            
        
        }

        [HttpGet("validatepass/{email}/{pass}/{confpass}")]
        public IActionResult validatePassInput(string email, string pass, string confpass)
        {
           
            bool result = false;
            bool theyaresame = false;

            if (pass == confpass)
            {
                theyaresame = true;
            }
            else 
            {
                theyaresame = false;
            }

            InputValidation inputValidation = new InputValidation();

            if (inputValidation.validatePassword(pass) && theyaresame && inputValidation.emailExists(email))
            {
                result = true;
            }
            else
            {
                result = false;
            }


            return new JsonResult(result);
        }


        [HttpPost("passwordchange")]
        public IActionResult PostPassword(RecoveryPassoward rp)
        {
            string m = "nothing";

            int userID = db.getUserId(rp.email);
            Console.WriteLine("did i get right ID " + userID);

            InputValidation checkValidation = new InputValidation();

            if (db.sendNewPasswordReset(rp, userID))
            {

                Console.WriteLine("Successfully");
                Console.WriteLine("did i get right ID " + userID);

                m = "successful";
            }
            else
            {
                m = "Error";
            }

            return new JsonResult(m);

        }
    }
}
