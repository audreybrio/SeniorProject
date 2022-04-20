using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.Registration;
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
        public IEnumerable<Registration> validateInput()
        {
            bool localUsername = false;
            bool localPassword = false;
            bool localEmail = false;
            bool localUniversity = false;

            return Enumerable.Range(1, 2).Select(index => new Registration
            {
                Username = localUsername,
                Password = localPassword,
                Email = localEmail,
                University = localUniversity
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
                Update usertoDB = new Update();
                usertoDB.UpdateCreate(email, password, username, university);
                return "Success";
            }catch(Exception ex)
            {
                return "Error creating new User" + ex.Message;
            }

        }
    }
}
