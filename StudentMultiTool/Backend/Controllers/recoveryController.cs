using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Controllers;
using StudentMultiTool.Backend.DAL;
using StudentMultiTool.Backend.Models.RecoveryAccount;
using StudentMultiTool.Backend.Services.Email;
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
            bool result = false;

            try
            {
                RecoveryDB db = new RecoveryDB();
                SendEmail e = new SendEmail();
                InputValidation inputValidation = new InputValidation();
              

                if (inputValidation.emailExists(r.email) && inputValidation.validateEmail(r.email))
                {
                    e.sendEmailPasswordReset(r.email);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new JsonResult(result);
        }

        [HttpGet("validate/{username}/{email}")]
        public IActionResult validateInput(string username, string email)
        {
            bool result = false;

            try
            {
                RecoveryDB db = new RecoveryDB();

                int userID = 0;


                InputValidation inputValidation = new InputValidation();


                if (inputValidation.validateEmail(email) && inputValidation.validateUsername(username) && inputValidation.usernameExists(username))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new JsonResult(result);


        }

        [HttpGet("validatepass/{email}/{pass}/{confpass}")]
        public IActionResult validatePassInput(string email, string pass, string confpass)
        {
            RecoveryDB db = new RecoveryDB();
            bool result = false;
            bool theyaresame = false;

            try
            {
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

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return new JsonResult(result);
        }


        [HttpPost("passwordchange")]
        public IActionResult PostPassword(RecoveryPassoward rp)
        {
            RecoveryDB db = new RecoveryDB();
            InputValidation inputValidation = new InputValidation();
            bool result = false;

            try
            {
                if (inputValidation.emailExists(rp.email))
                {
                    db.sendNewPasswordReset(rp, rp.email);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return new JsonResult(result);

        }


        [HttpPost("disabled")]
        public IActionResult PostDisabledEmail(RecoveryUserEmail r)
        {
            RecoveryDB db = new RecoveryDB();
            SendEmail e = new SendEmail();
            InputValidation inputValidation = new InputValidation();
            bool result = false;

            try
            {
                if (inputValidation.emailExists(r.email))
                {
                    e.sendEmailDisabledAccount(r.email);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new JsonResult(result);
        }

        [HttpPost("postactivate")]
        public IActionResult PostActivate(RecoveryUserEmail r)
        {
            bool result = false;
            LoginController lg = new LoginController();
            InputValidation inputValidation = new InputValidation();

            try
            {
                if (inputValidation.usernameExists(r.username))
                {
                    lg.UpdateDisableEnabled(r.username, r.actdiact);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new JsonResult(result);
        }
    }
}
