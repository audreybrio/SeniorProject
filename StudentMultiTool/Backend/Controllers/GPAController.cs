using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentMultiTool.Backend.Controllers
{
    public class GPAController : Controller
    {
        public IActionResult CalculateGPA([FromBody]  DataGrade grade)
        {
            return Ok();
        }

    }

    public class DataGrade
    {
        public List<string> Grade { get; set; }

        public List<string> Unit { get; set; }
    }
}
