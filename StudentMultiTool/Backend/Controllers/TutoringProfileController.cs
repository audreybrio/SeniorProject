using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Controllers

{
    public class TutoringProfileController : Controller
    {

        const string connectionString = "MARVELCONNECTIONSTRING";

        // Individual  is 1 if want individual, 0 if want group
        // requires is 1 if requires tutroing, 0 if offering
        public string TutoringProfile(string username, List<string> courses, bool individual, bool requires)
        {

            int listSize = courses.Count;
            Console.WriteLine(listSize);
            int remainder = 6 - listSize;
            for (int i = 0; i < remainder; i++)
            {
                courses.Add("null");
            }

            string course1 = courses[0];
            string course2 = courses[1];
            string course3 = courses[2];
            string course4 = courses[3];
            string course5 = courses[4];
            string course6 = courses[5];

            int userExists = ProfileExists(username);

            if (userExists != 0)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE TutoringProfile SET TutoringProfile.course1 = @course1, TutoringProfile.course2 = @course2, TutoringProfile.course3 = @course3, TutoringProfile.course4 = @course4, TutoringProfile.course5 = @course5, TutoringProfile.course6 = @course6, TutoringProfile.individual = @individual, TutoringProfile.requires = @requires WHERE userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
                cmd.Parameters.AddWithValue("@course1", course1);
                cmd.Parameters.AddWithValue("@course2", course2);
                cmd.Parameters.AddWithValue("@course3", course3);
                cmd.Parameters.AddWithValue("@course4", course4);
                cmd.Parameters.AddWithValue("@course5", course5);
                cmd.Parameters.AddWithValue("@course6", course6);
                cmd.Parameters.AddWithValue("@individual", individual);
                cmd.Parameters.AddWithValue("@requires", requires);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }

            else
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO TutoringProfile (userId, course1, course2, course3, course4, course5, course6, individual, requires) VALUES ( (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username), @course1, @course2, @course3, @course4, @course5, @course6, @individual, @requires)", conn);
                cmd.Parameters.AddWithValue("@course1", course1);
                cmd.Parameters.AddWithValue("@course2", course2);
                cmd.Parameters.AddWithValue("@course3", course3);
                cmd.Parameters.AddWithValue("@course4", course4);
                cmd.Parameters.AddWithValue("@course5", course5);
                cmd.Parameters.AddWithValue("@course6", course6);
                cmd.Parameters.AddWithValue("@individual", individual);
                cmd.Parameters.AddWithValue("@requires", requires);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }



            return "Success";
        }


        public int ProfileExists(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT (userId) FROM TutoringProfile WHERE userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            reader.Close();
            count = (int)cmd.ExecuteScalar();
            return count;
        }

        //// GET: TutoringProfileController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: TutoringProfileController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: TutoringProfileController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: TutoringProfileController/Create
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

        //// GET: TutoringProfileController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: TutoringProfileController/Edit/5
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

        //// GET: TutoringProfileController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: TutoringProfileController/Delete/5
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
