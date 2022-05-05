using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentMultiTool.Backend.Controllers
{
    //[ApiController]
    //[Route("api/grade")]
    public class GradeController : Controller
    {

        public bool CalculateGrade(string username, List<string> grades, List<string> outOf)
        {
            float total = 0;
            return true;
        }

        public List<string> DisplayRanking(string course)
        {
            var grades = new List<string>();
            return grades;
        }

        public bool SaveGrade(string username, string course, float grade)
        {
            return false;
        }

    }
}
