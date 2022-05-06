using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.DAL;
using UserAcc;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/userprivacy")]
    public class UserPrivacyController : Controller
    {
        [HttpGet]
        [Route("getOptions")]
        public IActionResult GetOptions()
        {
            return Ok();
        }

        [HttpPost]
        [Route("setOptions")]
        public IActionResult SetOptions()
        {
            return Ok();
        }

        [HttpPost]
        [Route("accountDeletion")]
        public IActionResult AccountDeletion([FromBody] string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("username cannot be null");
            }
            UserAccountDAO uad = new UserAccountDAO();
            UserAccount? user = uad.SelectSingle(username);
            if (user != null)
            {
                string result = uad.DeleteSingle(user);
                // TODO: data from other features
                if (result.Equals(uad.Success))
                {
                    return Ok(result);
                }
                return StatusCode(500, result);
            }
            return StatusCode(500, "Could not find user with username \"" + username + "\"");
        }
    }
}
