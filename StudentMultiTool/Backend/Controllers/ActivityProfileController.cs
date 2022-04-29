using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Services.Matching;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/" + "activityProfile")]
    public class ActivityProfileController : Controller
    {


        //[HttpPost("updateMain")]
        //public IActionResult HelpMe([FromBody] Data activities) {
        //    var x = Request.Body;
        //    return Ok("hello");
        //}

        [HttpPost("update/{username}/{opt}")]
        public IActionResult ActivityProfile([FromBody] Data activities, string username, bool opt)
        {
            //string username = "abrio";
            // bool opt = true;

            //int listSize = activities.activities.Count;
            //Console.WriteLine(listSize);    
            //int remainder = 5 - listSize;
            //for (int i = 0; i < remainder; i++)
            //{
            //    activities.activities.Add("null");
            //}

            //string activity1 = activities.activities[0];
            //string activity2 = activities.activities[1];
            //string activity3= activities.activities[2];
            //string activity4 = activities.activities[3];
            //string activity5 = activities.activities[4];

            //int userExists = ProfileExists(username);

            //if (userExists != 0)
            //{
            //    SqlConnection conn = new SqlConnection();
            //    conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("UPDATE ActivityProfile SET ActivityProfile.activity1 = @activity1, ActivityProfile.activity2 = @activity2, ActivityProfile.activity3 = @activity3, ActivityProfile.activity4 = @activity4, ActivityProfile.activity5 = @activity5 WHERE userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            //    cmd.Parameters.AddWithValue("@activity1", activity1);
            //    cmd.Parameters.AddWithValue("@activity2", activity2);
            //    cmd.Parameters.AddWithValue("@activity3", activity3);
            //    cmd.Parameters.AddWithValue("@activity4", activity4);
            //    cmd.Parameters.AddWithValue("@activity5", activity5);
            //    cmd.Parameters.AddWithValue("@username", username);
            //    cmd.ExecuteNonQuery();
            //}

            //else
            //{
            //    SqlConnection conn = new SqlConnection();
            //    conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("INSERT INTO ActivityProfile (userId, activity1, activity2, activity3, activity4, activity5, opt) VALUES ( (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username), @activity1, @activity2, @activity3, @activity4, @activity5, @opt)", conn);
            //    cmd.Parameters.AddWithValue("@activity1", activity1);
            //    cmd.Parameters.AddWithValue("@activity2", activity2);
            //    cmd.Parameters.AddWithValue("@activity3", activity3);
            //    cmd.Parameters.AddWithValue("@activity4", activity4);
            //    cmd.Parameters.AddWithValue("@activity5", activity5);
            //    cmd.Parameters.AddWithValue("@username", username);
            //    cmd.Parameters.AddWithValue("@opt", opt);
            //    cmd.ExecuteNonQuery();
            //}

            bool isActivityUpdated = Activity.ActivityProfile(activities.activities, username, opt);
            if (!isActivityUpdated)
            {
                return Ok();
            }

            return Ok();
        }
    }

    public class Data
    {
        public List<string> activities { get; set; }
    }
}
