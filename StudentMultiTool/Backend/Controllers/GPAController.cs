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
        public IActionResult CalculateGPA([FromBody]  DataGpa grade)
        {
            // Creates lists
            List<double> grades = new List<double>();
            List<int> units = new List<int>();  
            // Gets value for each grade
            for (int i = 0; i < grade.Grade.Count; i++)
            {
                if (grade.Grade[i] == "A+") { grades.Add(4.0); }
                else if (grade.Grade[i] == "A") { grades.Add(4.0); }
                else if (grade.Grade[i] == "A-") { grades.Add(3.7); }
                else if (grade.Grade[i] == "B+") { grades.Add(3.3); }
                else if (grade.Grade[i] == "B") { grades.Add(3.0); }
                else if (grade.Grade[i] == "B-") { grades.Add(2.7); }
                else if (grade.Grade[i] == "C+") { grades.Add(2.3); }
                else if (grade.Grade[i] == "C") { grades.Add(2.0); }
                else if (grade.Grade[i] == "C-") { grades.Add(1.7); }
                else if (grade.Grade[i] == "D+") { grades.Add(1.3); }
                else if (grade.Grade[i] == "D") { grades.Add(1.0); }
                else if (grade.Grade[i] == "D-") { grades.Add(0.7); }
                else if (grade.Grade[i] == "F") { grades.Add(0.0); }
            }


            // Gets value for unit
            for (int i = 0; i < grade.Unit.Count; i++)
            {
                units.Add(Int32.Parse(grade.Unit[i]));
                //Console.WriteLine(grade.Unit[i]);
            }
            // Calaculates Gpa
            GPA calcGpa = new GPA();
            double gpa = calcGpa.CalculateGPA(grades, units);
            Console.WriteLine(gpa);
            
            return Ok(gpa);
        }

    }

    // To get data from body
    public class DataGpa
    {
        public List<string> Grade { get; set; }

        public List<string> Unit { get; set; }
    }
}
