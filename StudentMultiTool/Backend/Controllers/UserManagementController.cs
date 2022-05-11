using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using UserAcc;
using StudentMultiTool.Backend.DAL;
using StudentMultiTool.Backend.Services.UserManagement;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/usermanagement")]
    public class UserManagementController : Controller
    {
        [HttpGet]
        [Route("getusers")]
        public List<UserAccount> GetUsers()
        {
            UserManager manager = new UserManager();
            return manager.GetUsers();
        }

        [HttpGet]
        [Route("getRoles")]
        public List<string> GetRoles()
        {
            UserManager manager = new UserManager();
            return manager.GetRoles();
        }

        [HttpPost]
        [Route("createUser")]
        public ActionResult CreateUser([FromBody] UserAccount user)
        {
            UserManager manager = new UserManager();
            string result = manager.CreateUser(user);
            if (result.Equals(manager.Success))
            {
                return Ok("User " + user.Username + " created successfully");
            }
            return StatusCode(500, result);
        }

        [HttpPost]
        [Route("updateUsers")]
        public ActionResult UpdateUsers([FromBody] List<UserAccount> users)
        {
            UserManager manager = new UserManager();
            string result = manager.UpdateUsers(users);
            if (result.Equals(manager.Success))
            {
                return Ok(result);
            }
            return StatusCode(500, result);
        }

        [HttpPost]
        [Route("deleteUsers")]
        public ActionResult DeleteUsers([FromBody] List<UserAccount> users)
        {
            UserManager manager = new UserManager();
            string result = manager.DeleteUsers(users);
            if (result.Equals(manager.Success))
            {
                return Ok(result);
            }
            return StatusCode(500, result);
        }

        [HttpPost]
        [Route("runBulkOperation")]
        public async Task<IActionResult> BulkOps(IFormFile file)
        {
            UserManager manager = new UserManager();
            string result = await manager.BulkOps(file);
            if (result.Equals(manager.Success))
            {
                return Ok(result);
            }
            return StatusCode(500, result);
        }
    }
}
