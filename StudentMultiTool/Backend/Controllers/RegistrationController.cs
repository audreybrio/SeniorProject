using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.Registration;
using StudentMultiTool.Backend.Services.Authentication;
using StudentMultiTool.Backend.Services.UserManagement;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Nodes;
using UserManagement;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/" + "registration")]
    public class RegistrationController : Controller
    {
        private readonly ILogger<Registration> _logger;
        public RegistrationController(ILogger<Registration> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRegistration")]
        public IEnumerable<Registration> Get()
        {
            return Enumerable.Range(1, 2).Select(index => new Registration
            {
                Username = true
            })
            .ToArray();
        }

        [HttpGet("validation/{username}/{password}/{email}/{university}")]
        public IEnumerable<Registration> validateInput(string username, string password, string email, string university)
        {
            bool localUsername = false;
            bool localPassword = false;
            bool localEmail = false;
            bool localUniversity = false;
            bool localEmailExist = false;
            bool localUsernameExist = false;

            InputValidation inputValidation = new InputValidation();
            if (inputValidation.validateUsername(username))
            {
                localUsername = true;
            }

            if (inputValidation.validatePassword(password))
            {
                localPassword = true;
            }

            if (inputValidation.validateEmail(email))
            {
                localEmail = true;
            }
            
            if (inputValidation.validateSchool(university))
            {
                localUniversity = true;
            }

            if (inputValidation.emailExists(email))
            {
                localEmailExist = true;
            }

            if (inputValidation.usernameExists(username))
            {
                localUsernameExist = true;
            }

            return Enumerable.Range(1, 1).Select(index => new Registration
            {
                Username = localUsername,
                Password = localPassword,
                Email = localEmail,
                University = localUniversity,
                EmailExist = localEmailExist,
                UsernameExist = localUsernameExist,
            })
            .ToArray();
        }


        // Create a new user.
        // Returns the status of the operation.
        [HttpPost]
        [HttpPost("newRegistration/{username}/{password}/{email}/{university}")]
        public string registerNewUser(string username, string password, string email, string university)
        {
            try
            {
                // Generates Unique ID token to verify user email
                String token = Guid.NewGuid().ToString();

                Update usertoDB = new Update();
                usertoDB.UpdateCreate(email, password, username, university, token);

                EmailVerification emailVerifycation = new EmailVerification();
                emailVerifycation.SendEmail(username, email, token, password);
                return "Success";
            }catch(Exception ex)
            {
                return "Error creating new User: " + ex.Message;
            }

        }

        // Activates user account if username and token matches.
        // Returns the status of the operation.
        [HttpPost]
        [HttpPost("emailVerification/{username}/{token}")]
        public string activateUserAccount(string username, string token)
        {
            try
            {
                Update manageAccount = new Update();
                if (manageAccount.ActivateAccount(username, token))
                {
                    return "Success";
                }
                else
                {
                    return "Error updating account";
                }

            }
            catch (Exception ex)
            {
                return "Error activating user's account: " + ex.Message;
            }
        }
    }
}
