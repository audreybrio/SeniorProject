using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/" + "activityProfile")]
    public class ActivityProfileController : Controller
    {
        const string connectionString = "MARVELCONNECTIONSTRING";

        [HttpPost("update/{activities}")]
        public IActionResult HelpMe(string[] activities) { 
            return Ok();
        }

        [HttpGet("update/{username}/{activities}/{opt}")]
        public IActionResult ActivityProfile(string username, List<string> activities, bool opt)
        {

            int listSize = activities.Count;
            Console.WriteLine(listSize);    
            int remainder = 5 - listSize;
            for (int i = 0; i < remainder; i++)
            {
                activities.Add("null");
            }

            string activity1 = activities[0];
            string activity2 = activities[1];
            string activity3= activities[2];
            string activity4 = activities[3];
            string activity5 = activities[4];

            int userExists = ProfileExists(username);

            if (userExists != 0)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE ActivityProfile SET ActivityProfile.activity1 = @activity1, ActivityProfile.activity2 = @activity2, ActivityProfile.activity3 = @activity3, ActivityProfile.activity4 = @activity4, ActivityProfile.activity5 = @activity5 WHERE userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
                cmd.Parameters.AddWithValue("@activity1", activity1);
                cmd.Parameters.AddWithValue("@activity2", activity2);
                cmd.Parameters.AddWithValue("@activity3", activity3);
                cmd.Parameters.AddWithValue("@activity4", activity4);
                cmd.Parameters.AddWithValue("@activity5", activity5);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }

            else
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ActivityProfile (userId, activity1, activity2, activity3, activity4, activity5, opt) VALUES ( (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username), @activity1, @activity2, @activity3, @activity4, @activity5, @opt)", conn);
                cmd.Parameters.AddWithValue("@activity1", activity1);
                cmd.Parameters.AddWithValue("@activity2", activity2);
                cmd.Parameters.AddWithValue("@activity3", activity3);
                cmd.Parameters.AddWithValue("@activity4", activity4);
                cmd.Parameters.AddWithValue("@activity5", activity5);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@opt", opt);
                cmd.ExecuteNonQuery();
            }



            return Ok();
        }

        public int ProfileExists(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT (userId) FROM ActivityProfile WHERE userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            reader.Close();
            count = (int)cmd.ExecuteScalar();
            return count;
        }




        //// GET: ActivityProfileController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: ActivityProfileController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: ActivityProfileController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ActivityProfileController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ActivityProfileController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ActivityProfileController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ActivityProfileController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ActivityProfileController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
