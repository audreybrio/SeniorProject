using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using StudentMultiTool.Backend.Models.Matching;
using StudentMultiTool.Backend.Controllers;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/" + "matching")]
    public class MatchingController : Controller
    {
        const string connectionString = "MARVELCONNECTIONSTRING";

        // matching logic for activity profile
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
                SqlCommand cmd = new SqlCommand("SELECT userId FROM ActivityProfile WHERE ActivityProfile.userId != (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND (activity1 = @activity OR activity2 = @activity OR activity3 = @activity OR activity4 = @activity OR activity5 = @activity) AND opt = @opt", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@activity", activities[i]);
                cmd.Parameters.AddWithValue("@opt", 1);
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
                    int countMatchExists = MatchExists(username, id, activities[i]);
                    if (countMatchExists == 0){
                        InsertMatch(username, id, activities[i]);
                    }
                    
                }   
            }
            
            foreach (KeyValuePair<int, List<string>> kvp in matches)
                foreach (string val in kvp.Value)
                    Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, val);

            return true;
        }

        // Matching Logic for Tutoring profile
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
                SqlCommand cmd = new SqlCommand("SELECT userId FROM TutoringProfile WHERE TutoringProfile.userId != (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND (course1 = @course OR course2 = @course OR course3 = @course OR course4 = @course OR course5 = @course OR course6 = @course) AND individual = @individual AND requires != @requires AND opt = @opt", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@course", courses[i]);
                cmd.Parameters.AddWithValue("@individual", individual);
                cmd.Parameters.AddWithValue("@requires", requires);
                cmd.Parameters.AddWithValue("@opt", 1);
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

        // Display Matches to frontend 
        public List<Match> DisplayMatches(string username)
        {
            List<Match> matches = new List<Match>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT matchId, reason FROM Matches WHERE  Matches.userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            cmd.Parameters.AddWithValue("@username", username);
            int matchId = 0;
            string reason = "";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            { 
                matchId = dr.GetInt32(0);
                reason = dr.GetString(1);

                Match matchExample = new Match(matchId, reason);
                matches.Add(matchExample);

            }
            dr.Close();
            foreach (Match match in matches)
            {
                Console.WriteLine(match.matchId + " " + match.reason);
            }
            return matches;
        }

        // Get activity profile
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

        // Get tutoring profile
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

        // Insert Match into database
        public void InsertMatch(string username, int matchId, string reason)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Matches (userId, matchId, reason, overlap) VALUES ((SELECT id FROM UserAccounts WHERE UserAccounts.username = @username), @matchId, @reason, NULL)", conn);
            cmd.Parameters.AddWithValue("@matchId", matchId);
            cmd.Parameters.AddWithValue("@reason", reason);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
        }

        public int MatchExists(string username, int matchId, string reason)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(userId) FROM Matches WHERE userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND matchId = @matchId AND reason = @reason ", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@matchId", matchId);
            cmd.Parameters.AddWithValue("@reason", reason);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            reader.Close();
            count = (int)cmd.ExecuteScalar();
            return count;
        }

        // when generate matches, checking if opted in 
        public bool CheckOptedIn(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT opt FROM TutoringProfile WHERE TutoringProfile.userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            cmd.Parameters.AddWithValue("@username", username);
            bool opt = (bool) cmd.ExecuteScalar();
            return opt; 
        }


        // For when user wants to opt in / out 
        [HttpGet("updateOptStatus/{username}/{opt}")]
        public IActionResult UpdateOptStatus(string username, bool opt)
        {

            int countActivityProfile = ActivityProfileController.ProfileExists(username);
            int countTutoringProfile = TutoringProfileController.ProfileExists(username);

            if (countActivityProfile == 0 && countTutoringProfile == 0)
            {
                return NotFound();
            }
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE TutoringProfile SET opt = @opt WHERE TutoringProfile.userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@opt", opt);
            cmd.ExecuteScalar();
            SqlCommand cmd2 = new SqlCommand("UPDATE ActivityProfile SET opt = @opt WHERE ActivityProfile.userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            cmd2.Parameters.AddWithValue("@username", username);
            cmd2.Parameters.AddWithValue("@opt", opt);
            cmd2.ExecuteScalar();
            return Ok();
        }

    }
}
