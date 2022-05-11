using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.DAL;
using StudentMultiTool.Backend.Services.UserPrivacy;
using UserAcc;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/userprivacy")]
    public class UserPrivacyController : Controller
    {
        [HttpGet]
        [Route("getOptions")]
        public PrivacyOptions GetOptions([FromQuery] string username)
        {

            UserPrivacyManager userPrivacyManager = new UserPrivacyManager();
            PrivacyOptions options = new PrivacyOptions();

            // Try to get the privacy options for the current user.
            options = userPrivacyManager.GetOptions(username);
            return options;
        }

        [HttpPost]
        [Route("setOptions")]
        public IActionResult SetOptions(PrivacyOptions options)
        {
            UserPrivacyManager userPrivacyManager = new UserPrivacyManager();

            // Try to update the privacy options for the current user.
            string result = userPrivacyManager.SetOptions(options);
            if (result.Equals(userPrivacyManager.Success))
            {
                return Ok();
            }
            return StatusCode(500, result);
        }

        [HttpPost]
        [Route("accountDeletion")]
        public IActionResult AccountDeletion([FromQuery] string username)
        {
            UserPrivacyManager userPrivacyManager = new UserPrivacyManager();

            // Try to delete the current user's account.
            string result = userPrivacyManager.DeleteAccount(username);
            if (result.Equals(userPrivacyManager.Success))
            {
                return Ok();
            }
            return StatusCode(500, result);
        }
    }
}
