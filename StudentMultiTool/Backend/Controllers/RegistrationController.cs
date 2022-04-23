using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.Registration;
using StudentMultiTool.Backend.Services.Authentication;
using StudentMultiTool.Backend.Services.UserManagement;
using System.Data;
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

        // validateInput method returns an array of IEnumerable with the valid or invalid
        // values for the user's input
        [HttpGet("validation/{username}/{password}/{email}/{university}")]
        public IEnumerable<Registration> validateInput(string username, string password, string email, string university)
        {
            bool localUsername = false;
            bool localPassword = false;
            bool localEmail = false;
            bool localUniversity = false;
            bool localEmailExist = false;
            bool localUsernameExist = false;

            // If statements to verify each user's input
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

            // Returns the array of valid or invalid input values
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


        // Create a new user and sends email verification.
        // Returns the status of the operation.
        [HttpPost]
        [HttpPost("newRegistration/{username}/{password}/{email}/{university}")]
        public IActionResult registerNewUser(string username, string password, string email, string university)
        {
            try
            {
                // Generates Unique ID token to verify user email
                String token = Guid.NewGuid().ToString();

                // Creates a new user account in the UserAccounts table
                Update usertoDB = new Update();
                usertoDB.UpdateCreate(email, password, username, university, token);

                // Sends the email verification to the user's email address
                EmailVerification emailVerifycation = new EmailVerification();
                emailVerifycation.SendEmail(username, email, token, password);
                return Ok("Success");
            }catch(Exception ex)
            {
                return NotFound();
            }

        }

        // Activates user account if username and token matches.
        // Returns the status of the operation.
        [HttpPost]
        [HttpPost("emailVerification/{username}/{token}")]
        public IActionResult activateUserAccount(string username, string token)
        {
            try
            {
                // the username and token matches with our database values
                // the new user account is activated
                Update manageAccount = new Update();
                if (manageAccount.ActivateAccount(username, token))
                {
                    return Ok("Success");
                }
                else
                {
                    return Ok("Error");
                }

            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
