using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using StudentMultiTool.Backend.Models.Matching;
using StudentMultiTool.Backend.Services.Matching;


// Matching Controller 
namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/" + "matching")]
    public class MatchingController : Controller
    {
 
        // Matches activity profiles together 
        [HttpGet("matchActivity/{username}")]
        public List<Match> MatchingActivity(string username)
        {
            List<Match> matches = new List<Match>();
            matches = Matching.MatchingActivity(username);
            return matches;

        }

        // Matching Tutoring profiles together 
        [HttpGet("matchTutoring/{username}")]
        public List<Match> MatchingTutoring(string username)
        {
            List<Match> matches = new List<Match>();
            matches = Matching.MatchingTutoring(username);
            return matches;

        }

         // Display Matches to frontend 
        [HttpGet("displayMatches/{username}")]
        public List<Match> DisplayMatches(string username)
        {
            List<Match> matches = new List<Match>();
            matches = Matching.DisplayMatches(username);
            return matches;
        }


        // For when user wants to opt in / out 
        [HttpGet("updateOptStatus/{username}/{opt}")]
        public IActionResult UpdateOptStatus(string username, bool opt)
        {
            bool updateOpt = Matching.UpdateOptStatus(username, opt);
            if (!updateOpt)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
