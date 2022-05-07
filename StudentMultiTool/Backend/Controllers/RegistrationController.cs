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

        // validateInput method returns an array of IEnumerable with the valid or invalid
        // values for the user's input
        [HttpGet("validation/{email}/{passcode}/{university}")]
        public IEnumerable<Registration> validateInput(string email, string passcode, string university)
        {
            bool localPasscode = false;
            bool localEmail = false;
            bool localUniversity = false;
            bool localEmailExist = false;

            // If statements to verify each user's input
            InputValidation inputValidation = new InputValidation();
            if (inputValidation.validatePasscode(passcode))
            {
                localPasscode = true;
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


            // Returns the array of valid or invalid input values
            return Enumerable.Range(1, 1).Select(index => new Registration
            {
                Passcode = localPasscode,
                Email = localEmail,
                University = localUniversity,
                EmailExist = localEmailExist
            })
            .ToArray();
        }


        // Create a new user and sends email verification.
        // Returns the status of the operation.
        [HttpGet]
        [HttpGet("newRegistration/{email}/{passcode}/{university}")]
        public IActionResult newRegistration(string email, string passcode, string university)
        {
            try
            {
                Console.WriteLine("In new registration");
                // Generates Unique ID token to verify user email
                String token = Guid.NewGuid().ToString();

                // Creates a new user account in the UserAccounts table
                Update usertoDB = new Update();
                usertoDB.UpdateCreate(email, passcode, university, token);

                // Sends the email verification to the user's email address
                EmailVerification emailVerifycation = new EmailVerification();
                emailVerifycation.SendEmail(email, token);
                return Ok("Success");
            }catch(Exception ex)
            {
                return NotFound();
            }

        }

        // Activates user account if username and token matches.
        // Returns the status of the operation.
        [HttpGet]
        [HttpGet("emailVerification/{token}")]
        public IActionResult activateUserAccount(string token)
        {
            try
            {
                //the username and token matches with our database values
                //the new user account is activated
                Update manageAccount = new Update();
                if (manageAccount.ActivateAccount(token))
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
