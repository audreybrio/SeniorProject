using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Services.GPA_Calc;
using StudentMultiTool.Backend.Models.GPACalc;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/" + "gradeCalc")]
    public class GradeController : Controller
    {
        [HttpPost("calculateGrade")]
        public IActionResult CalculateGrade([FromBody] DataGrade assignment)
        {

            try
            {
                List<double> assignments = new List<double>();
                List<int> points = new List<int>();

                for (int i = 0; i < assignment.assignments.Count; i++)
                {
                    assignments.Add(Double.Parse(assignment.assignments[i]));
                    Console.WriteLine(assignment.assignments[i]);
                }

                for (int i = 0; i < assignment.points.Count; i++)
                {
                    points.Add(Int32.Parse(assignment.points[i]));
                    Console.WriteLine(assignment.points[i]);
                }
                Grade calcGrade = new Grade();
                double grade = calcGrade.CalculateGrade(assignments, points);
                return Ok(grade);
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpPost("saveGrade/{username}/{course}/{section}")]
        public IActionResult SaveeGrade([FromBody] DataGrade assignment, string username, string course, string section)
        {

            try
            {
                List<double> assignments = new List<double>();
                List<int> points = new List<int>();

                for (int i = 0; i < assignment.assignments.Count; i++)
                {
                    assignments.Add(Double.Parse(assignment.assignments[i]));
                }

                for (int i = 0; i < assignment.points.Count; i++)
                {
                    points.Add(Int32.Parse(assignment.points[i]));
                }
                Grade calcGrade = new Grade();
                double grade = calcGrade.CalculateGrade(assignments, points);
                bool gradeSaved = calcGrade.SaveGrade(username, course, grade, Int32.Parse(section));
                if (!gradeSaved)
                {
                    return NotFound();
                }
                return Ok(grade);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpGet("displayRanking/{course}/{section}")]
        public List<GradeModel> DisplayRanking(string course, string section)
        {
            List<GradeModel> ranking = new List<GradeModel>();
            Grade grade = new Grade();
            ranking = grade.DisplayRanking(course, Int32.Parse(section));
            return ranking;
        }


    }
    // To get data from body
    public class DataGrade
    {
        public List<string> assignments { get; set; }

        public List<string> points { get; set; }
    }
}
