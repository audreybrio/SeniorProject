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

            RecoveryDB db = new RecoveryDB();

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
            RecoveryDB db = new RecoveryDB();

            string m = "nothing";

            int userID = db.getUserId(rp.email);
            Console.WriteLine("did i get right ID " + userID);

            InputValidation checkValidation = new InputValidation();

            if (db.sendNewPasswordReset(rp, userID))
            {

          

                m = "successful";
            }
            else
            {
                m = "Error";
            }

            return new JsonResult(m);

        }


        [HttpPost("disabled")]
        public IActionResult PostDisabled(RecoveryUserEmail r)
        {
            RecoveryDB db = new RecoveryDB();

            string m = "nothing";

            if (db.sendEmailDisabledAccount(r))
            {
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
            UserAccountDAO userDB = new UserAccountDAO();
            UserAccount u = new UserAccount();
            string m = "nothing";

            Console.WriteLine("user== "+ r.username);

            Console.WriteLine("Activate== " + r.activate);

            u = userDB.SelectSingle(r.username);

            u.active = r.activate;
           
            if (userDB.UpdateSingle(u) == "Success")
            {
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
