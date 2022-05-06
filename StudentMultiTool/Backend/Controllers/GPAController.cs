using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Services.GPA_Calc;

namespace StudentMultiTool.Backend.Controllers
{

    [ApiController]
    [Route("api/" + "gpaCalc")]
    public class GPAController : Controller
    {
        [HttpPost("calculateGPA")]
        public IActionResult CalculateGPA([FromBody]  DataGrade grade)
        {
            List<string> grades = new List<string>();
            List<string> units = new List<string>();    
            for (int i = 0; i < grade.Grade.Count; i++)
            {
                grades.Add(grade.Grade[i]);
                //Console.WriteLine(grade.Grade[i]);
            }

            for (int i = 0; i < grade.Unit.Count; i++)
            {
                units.Add(grade.Unit[i]);
                //Console.WriteLine(grade.Unit[i]);
            }

            GPA calcGpa = new GPA();
            double gpa = calcGpa.CalculateGPA(grades, units);
            Console.WriteLine(gpa);
            
            return Ok(gpa);
        }

    }

    public class DataGrade
    {
        public List<string> Grade { get; set; }

        public List<string> Unit { get; set; }
    }
}
