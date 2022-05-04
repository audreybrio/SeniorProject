using Microsoft.AspNetCore.Mvc;
using RecipeDetails.Models;

namespace RecipeDetails.Controllers
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

        RecoveryDB db = new RecoveryDB();

        [HttpPost("reset")]
        public IActionResult Post(RecoveryUserEmail r)
        {
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


        [HttpPost("passwordchange")]
        public IActionResult PostPassword(RecoveryPassoward rp)
        {
            string m = "nothing";

            if (db.sendNewPasswordReset(rp))
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
