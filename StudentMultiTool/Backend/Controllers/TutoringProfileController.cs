using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Services.Matching;

namespace StudentMultiTool.Backend.Controllers

{
    [ApiController]
    [Route("api/" + "tutoringProfile")]
    public class TutoringProfileController : Controller
    {

        // Individual  is 1 if want individual, 0 if want group
        // requires is 1 if requires tutroing, 0 if offering

        [HttpPost]
        [Route("update/{username}/{individual}/{requires}/{opt}")]
        public IActionResult TutoringProfile([FromBody] DataT courses, string username, bool individual, bool requires, bool opt)
        {

            bool updateTutoring;
            updateTutoring = Tutoring.TutoringProfile(courses.courses, username, individual, requires, opt);
            if (!updateTutoring)
            {
                return Ok();
            }

            return Ok();
            //string username = "abrio";
            //bool individual = true;
            //bool requires = true;
            //bool opt = true;

            //int listSize = courses.courses.Count;
            //Console.WriteLine(listSize);
            //int remainder = 6 - listSize;
            //for (int i = 0; i < remainder; i++)
            //{
            //    courses.courses.Add("null");
            //}

            //string course1 = courses.courses[0];
            //string course2 = courses.courses[1];
            //string course3 = courses.courses[2];
            //string course4 = courses.courses[3];
            //string course5 = courses.courses[4];
            //string course6 = courses.courses[5];

            //int userExists = ProfileExists(username);

            //if (userExists != 0)
            //{
            //    SqlConnection conn = new SqlConnection();
            //    conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("UPDATE TutoringProfile SET TutoringProfile.course1 = @course1, TutoringProfile.course2 = @course2, TutoringProfile.course3 = @course3, TutoringProfile.course4 = @course4, TutoringProfile.course5 = @course5, TutoringProfile.course6 = @course6, TutoringProfile.individual = @individual, TutoringProfile.requires = @requires WHERE userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            //    cmd.Parameters.AddWithValue("@course1", course1);
            //    cmd.Parameters.AddWithValue("@course2", course2);
            //    cmd.Parameters.AddWithValue("@course3", course3);
            //    cmd.Parameters.AddWithValue("@course4", course4);
            //    cmd.Parameters.AddWithValue("@course5", course5);
            //    cmd.Parameters.AddWithValue("@course6", course6);
            //    cmd.Parameters.AddWithValue("@individual", individual);
            //    cmd.Parameters.AddWithValue("@requires", requires);
            //    cmd.Parameters.AddWithValue("@username", username);
            //    cmd.ExecuteNonQuery();
            //}

            //else
            //{
            //    SqlConnection conn = new SqlConnection();
            //    conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("INSERT INTO TutoringProfile (userId, course1, course2, course3, course4, course5, course6, individual, requires, opt) VALUES ( (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username), @course1, @course2, @course3, @course4, @course5, @course6, @individual, @requires, @opt)", conn);
            //    cmd.Parameters.AddWithValue("@course1", course1);
            //    cmd.Parameters.AddWithValue("@course2", course2);
            //    cmd.Parameters.AddWithValue("@course3", course3);
            //    cmd.Parameters.AddWithValue("@course4", course4);
            //    cmd.Parameters.AddWithValue("@course5", course5);
            //    cmd.Parameters.AddWithValue("@course6", course6);
            //    cmd.Parameters.AddWithValue("@individual", individual);
            //    cmd.Parameters.AddWithValue("@requires", requires);
            //    cmd.Parameters.AddWithValue("@username", username);
            //    cmd.Parameters.AddWithValue("@opt", opt);
            //    cmd.ExecuteNonQuery();
            //}

        }


    }
    public class DataT
    {
        public List<string> courses { get; set; }
    }
}
