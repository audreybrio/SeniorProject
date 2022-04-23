using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Controllers
{
    public class MatchingController : Controller
    {
        const string connectionString = "MARVELCONNECTIONSTRING";
        public bool MatchingActivity(string username)
        {

            List<string> activities = new List<string>();
            Dictionary<int, List<string>> matches = new Dictionary<int, List<string>>();
            activities = GetActivityProfile(username);

            for (int i = 0; i < activities.Count; i++)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT userId FROM ActivityProfile WHERE ActivityProfile.userId != (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND (activity1 = @activity OR activity2 = @activity OR activity3 = @activity OR activity4 = @activity OR activity5 = @activity)", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@activity", activities[i]);
                cmd.ExecuteNonQuery();
                if(cmd.ExecuteScalar() != null)
                {
                    int id = (int)cmd.ExecuteScalar();
                    if (matches.ContainsKey(id))
                    {
                        List<string> list = matches[id];
                        list.Add(activities[i]);
                    }
                    else
                    {
                        matches.Add(id, new List<string> { activities[i] });
                    }
                }   
            }

            foreach (KeyValuePair<int, List<string>> kvp in matches)
                foreach (string val in kvp.Value)
                    Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, val);

            return true;
        }

        public bool MatchingTutoring(string username)
        {
            List<string> courses = new List<string>();
            bool individual, requires;
            Dictionary<int, List<string>> matches = new Dictionary<int, List<string>>();
            (courses, individual, requires) = GetTutoringProfile(username);

            for (int i = 0; i < courses.Count; i++)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT userId FROM TutoringProfile WHERE TutoringProfile.userId != (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND (course1 = @course OR course2 = @course OR course3 = @course OR course4 = @course OR course5 = @course OR course6 = @course) AND individual = @individual AND requires != @requires", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@course", courses[i]);
                cmd.Parameters.AddWithValue("@individual", individual);
                cmd.Parameters.AddWithValue("@requires", requires);
                cmd.ExecuteNonQuery();
                if (cmd.ExecuteScalar() != null)
                {
                    int id = (int)cmd.ExecuteScalar();
                    if (matches.ContainsKey(id))
                    {
                        List<string> list = matches[id];
                        list.Add(courses[i]);
                    }
                    else
                    {
                        matches.Add(id, new List<string> { courses[i] });
                    }
                    
                }
            }

            foreach (KeyValuePair<int, List<string>> kvp in matches)
                foreach (string val in kvp.Value)
                    Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, val);

            return true;
        }

        public bool DisplayMatches()
        {
            return true;
        }

        public List<string> GetActivityProfile(string username)
        {
            List<string> activities = new List<string>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT activity1, activity2, activity3, activity4, activity5 FROM ActivityProfile WHERE ActivityProfile.userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            cmd.Parameters.AddWithValue("@username", username);
            string activity1 = "", activity2 = "", activity3 = "", activity4 = "", activity5 = "";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                activity1 = dr.GetString(0);
                activity2 = dr.GetString(1);
                activity3 = dr.GetString(2);
                activity4 = dr.GetString(3);
                activity5 = dr.GetString(4);
            }
            dr.Close();


            activities.Add(activity1);
            if(activity2 != "null")
            {
                activities.Add(activity2);
            }
            if(activity3 != "null")
            {
                activities.Add(activity3);
            }
            if (activity4 != "null")
            {
                activities.Add(activity4);
            }
            if (activity5 != "null")
            {
                activities.Add(activity5);
            }
            
            foreach(string activity in activities)
            {
                Console.WriteLine(activity);
            }
            return activities;
        }


        public (List<string>, bool, bool) GetTutoringProfile(string username)
        {

            List<string> courses = new List<string>();
            // return (courses, true, true);
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT course1, course2, course3, course4, course5, course6, individual, requires FROM TutoringProfile WHERE TutoringProfile.userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            cmd.Parameters.AddWithValue("@username", username);
            string course1 = "", course2 = "", course3 = "", course4 = "", course5 = "", course6 = "";
            bool individual = false, requires = false;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                course1 = dr.GetString(0);
                course2 = dr.GetString(1);
                course3 = dr.GetString(2);
                course4 = dr.GetString(3);
                course5 = dr.GetString(4);
                course6 = dr.GetString(5);
                individual = dr.GetBoolean(6);
                requires = dr.GetBoolean(7);
            }
            dr.Close();


            courses.Add(course1);
            if (course2 != "null")
            {
                courses.Add(course2);
            }
            if (course3 != "null")
            {
                courses.Add(course3);
            }
            if (course4 != "null")
            {
                courses.Add(course4);
            }
            if (course5 != "null")
            {
                courses.Add(course5);
            }
            if(course6 != "null")
            {
                courses.Add(course6);
            }

            foreach (string activity in courses)
            {
                Console.WriteLine(activity);
            }
            return (courses, individual, requires);

        }

        public bool InsertMatch(string username)
        {
            return true;
        }
        //// GET: MatchingController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: MatchingController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: MatchingController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: MatchingController/Create
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

        //// GET: MatchingController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: MatchingController/Edit/5
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

        //// GET: MatchingController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: MatchingController/Delete/5
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
