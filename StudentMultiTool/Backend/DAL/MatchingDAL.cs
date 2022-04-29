using StudentMultiTool.Backend.Controllers;
using StudentMultiTool.Backend.Services;
using StudentMultiTool.Backend.Models.Matching;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.DAL
{
    public class MatchingDAL
    {
        const string connectionString = "MARVELCONNECTIONSTRING";

        // SQL for matching activity logic
        public static bool MatchingActivity(List<string> activities, string username)
        {
            // Loops through selected activities to find matches 
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();

            for (int i = 0; i < activities.Count; i++)
            {
                //SqlConnection conn = new SqlConnection();
                //conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                //conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT userId FROM ActivityProfile WHERE ActivityProfile.userId != (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND (activity1 = @activity OR activity2 = @activity OR activity3 = @activity OR activity4 = @activity OR activity5 = @activity) AND opt = @opt", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@activity", activities[i]);
                cmd.Parameters.AddWithValue("@opt", 1);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int countMatchExists = MatchExists(username, id, activities[i]);
                        if (countMatchExists == 0)
                        {
                            string overlap = Match.GetOverlap(username, id);
                            InsertMatch(username, id, activities[i], overlap);
                        }

                    }

                }
                catch (Exception ex)
                {
                    return false;
                }
                dr.Close();
            }
            conn.Close();
            return true;
        }

        // Matching Logic for Tutoring profile
        public static bool MatchingTutoring(List<string> courses, string username, bool individual, bool requires)
        {
            // Loops through prfile given for tutoring to find matches 
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            for (int i = 0; i < courses.Count; i++)
            {
                SqlCommand cmd = new SqlCommand("SELECT userId FROM TutoringProfile WHERE TutoringProfile.userId != (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND (course1 = @course OR course2 = @course OR course3 = @course OR course4 = @course OR course5 = @course OR course6 = @course) AND individual = @individual AND requires != @requires AND opt = @opt", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@course", courses[i]);
                cmd.Parameters.AddWithValue("@individual", individual);
                cmd.Parameters.AddWithValue("@requires", requires);
                cmd.Parameters.AddWithValue("@opt", 1);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int countMatchExists = MatchExists(username, id, courses[i]);
                        if (countMatchExists == 0)
                        {
                            string overlap = Match.GetOverlap(username, id);
                            InsertMatch(username, id, courses[i], overlap);
                        }
                    }
                }
                catch (Exception e) { return false; }
                dr.Close();
            }
            
            conn.Close();
            return true;
        }

        // Display Matches to frontend 
        public static List<Match> DisplayMatches(string username)
        {
            //string username = "abrio";
            List<Match> matches = new List<Match>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT matchId, reason, overlap FROM Matches WHERE  Matches.userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            cmd.Parameters.AddWithValue("@username", username);
            int matchId = 0;
            string reason = "";
            string overlap = "";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                matchId = dr.GetInt32(0);
                reason = dr.GetString(1);
                overlap = dr.GetString(2);


                string matchName = Match.GetName(matchId);
                Match newMatch = new Match(matchName, reason, overlap);
                matches.Add(newMatch);

            }
            dr.Close();
            conn.Close();
            return matches;
        }

        // Get activity profile
        public static (string, string, string, string, string) GetActivityProfile(string username)
        {
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
            conn.Close();
            
            return (activity1, activity2, activity3, activity4, activity5);
        }

        // Get tutoring profile
        public static (string, string, string, string, string, string, bool, bool) GetTutoringProfile(string username)
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
            conn.Close();
            return (course1, course2, course3, course4, course5, course6, individual, requires);

        }

        // Insert Match into database
        public static bool InsertMatch(string username, int matchId, string reason, string overlap)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Matches (userId, matchId, reason, overlap) VALUES ((SELECT id FROM UserAccounts WHERE UserAccounts.username = @username), @matchId, @reason, @overlap)", conn);
            cmd.Parameters.AddWithValue("@matchId", matchId);
            cmd.Parameters.AddWithValue("@reason", reason);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@overlap", overlap);
            
            try
            {
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }

            catch
            {
                conn.Close();   
                return false;
            }

        }

        public static int MatchExists(string username, int matchId, string reason)
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
            conn.Close();
            return count;
        }

        // almost whole thing  can go 
        public static bool CheckOptedIn(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT opt FROM TutoringProfile WHERE TutoringProfile.userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            cmd.Parameters.AddWithValue("@username", username);
            bool opt = (bool)cmd.ExecuteScalar();
            conn.Close();
            return opt;
        }


        // For when user wants to opt in / out 
        public static bool UpdateOptStatus(string username, bool opt)
        {
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
            conn.Close();  
            return true;
        }
    }
}
