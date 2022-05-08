using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Controllers;
using StudentMultiTool.Backend.DAL;
using StudentMultiTool.Backend.Models.RecoveryAccount;
using StudentMultiTool.Backend.Models.Registration;
using StudentMultiTool.Backend.Services.Authentication;
using StudentMultiTool.Backend.Services.UserManagement;
using System.Data;
using UserAcc;


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



        [HttpPost("reset")]
        public IActionResult Post(RecoveryUserEmail r)
        {
            RecoveryDB db = new RecoveryDB();
            EmailVerification email = new EmailVerification();
            InputValidation inputValidation = new InputValidation();

            string m = "nothing";

            if (inputValidation.emailExists(r.email) && inputValidation.validateEmail(r.email))
            {
                email.sendEmailPasswordReset(r.email);
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

            RecoveryDB db = new RecoveryDB();

            bool result = false;
            int userID  = 0;


            InputValidation inputValidation = new InputValidation();

            
            if (inputValidation.validateEmail(email) && inputValidation.validateUsername(username) && inputValidation.usernameExists(username))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return new JsonResult(result); 
            
        
        }

        [HttpGet("validatepass/{email}/{pass}/{confpass}")]
        public IActionResult validatePassInput(string email, string pass, string confpass)
        {
            RecoveryDB db = new RecoveryDB();

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

            if (inputValidation.validatePasscode(pass) && theyaresame && inputValidation.validateEmail(email))
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
            RecoveryDB db = new RecoveryDB();
            InputValidation inputValidation = new InputValidation();

            string m = "nothing";

            if (inputValidation.emailExists(rp.email))
            {
                db.sendNewPasswordReset(rp, rp.email);
                m = "successful";
            }
            else
            {
                m = "Error";
            }

            return new JsonResult(m);

        }


        [HttpPost("disabled")]
        public IActionResult PostDisabledEmail(RecoveryUserEmail r)
        {
            RecoveryDB db = new RecoveryDB();
            EmailVerification e = new EmailVerification();
            InputValidation inputValidation = new InputValidation();

            string m = "nothing";

            if (inputValidation.emailExists(r.email))
            {
                e.sendEmailDisabledAccount(r.email);
                m = "successful";
            }
            else
            {
                m = "Error";
            }

            return new JsonResult(m);
        }

        [HttpPost("postactivate")]
        public IActionResult PostActivate(RecoveryUserEmail r)
        {

            LoginController lg = new LoginController();
            string m = "nothing";

            InputValidation inputValidation = new InputValidation();

            if (inputValidation.usernameExists(r.username))
            {
                lg.UpdateDisableEnabled(r.username, r.actdiact);

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
