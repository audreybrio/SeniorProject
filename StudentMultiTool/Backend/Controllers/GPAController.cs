using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentMultiTool.Backend.Controllers
{
    public class GPAController : Controller
    {
        public IActionResult CalculateGPA(string username, List<string> grades, List<string> units)
        {
            return Ok();
        }

    }
}
