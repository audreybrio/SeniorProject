using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.UAD;
using StudentMultiTool.Backend.Services.UsageAnalysisDashboard;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/uad")]
    public class UADController : Controller
    {

        UAD usage = new UAD();

        // Most visited pages
        [HttpGet("mostVisited")]
        public IActionResult MostVisited()
        {
            GraphBar mostVisited = new GraphBar();
            mostVisited = usage.MostVisited();
            return Ok(mostVisited);
        }

        // Top schools
        [HttpGet("topSchool")]
        public IActionResult TopSchool()
        {
            GraphBar topSchools = new GraphBar();
            topSchools = usage.TopSchool();
            return Ok(topSchools);
        }

        // Average duration on a page
        [HttpGet("averageDuration")]
        public IActionResult AverageDuration()
        {
            GraphBar average = new GraphBar();
            average = usage.AverageDuration();
            return Ok(average);
        }

        // Number of logins over the past 3 months
        [HttpGet("numLogin")]
        public IActionResult NumLogin()
        {
            GraphTrend login = new GraphTrend();
            login = usage.NumLogin();
            return Ok(login);
        }
        // Number of Matches over the past 3 months
        [HttpGet("numMatches")]
        public IActionResult NumMatches()
        {
            GraphTrend matches = new GraphTrend();
            matches = usage.NumMatches();
            return Ok(matches);
        }

        // Number of Registrations over the past 3 months
        [HttpGet("numRegister")]
        public IActionResult numRegister()
        {
            GraphTrend register = new GraphTrend();
            register = usage.NumRegister();
            return Ok(register);
        }


    }
}
