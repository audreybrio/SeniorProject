using StudentMultiTool.Backend.Controllers;
using StudentMultiTool.Backend.Services.ScheduleComparison;
using StudentMultiTool.Backend.Models.Matching;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.DAL
{
    // Matching DAL
    public class MatchingDAL
    {
        // Connection string for database
        const string connectionString = "MARVELCONNECTIONSTRING";

        // SQL for matching activities logic
        public List<Match> MatchingActivity(List<string> activities, string username)
        {
            // Loops through selected activities to find matches 
            List<Match> matches = new List<Match>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();

            for (int i = 0; i < activities.Count; i++)
            {               
                try
                {
                // Gets list of users also have the same activity in their profile
                SqlCommand cmd = new SqlCommand("SELECT userId FROM ActivityProfile WHERE ActivityProfile.userId != (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND (activity1 = @activity OR activity2 = @activity OR activity3 = @activity OR activity4 = @activity OR activity5 = @activity) AND opt = @opt", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@activity", activities[i]);
                cmd.Parameters.AddWithValue("@opt", 1);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string matchName = GetName(id);
                        string overlap = ScheduleComparing.GetOverlap(username, id);
                        Match newMatch = new Match(matchName, activities[i], overlap);
                        matches.Add(newMatch);
                        int countMatchExists = MatchExists(username, id, activities[i]);
                        // If match doesnt already exist, insert into database
                        if (countMatchExists == 0)
                        {
                            // string overlap = ScheduleComparing.GetOverlap(username, id);
                            InsertMatch(username, id, activities[i], overlap);
                        }

                    }
                    dr.Close();

                }
                catch (Exception ex)
                {
                    return matches;
                }
                
            }
            conn.Close();
            return matches;
        }

        // SQL for matching tutoring profiles
        public List<Match> MatchingTutoring(List<string> courses, string username, bool individual, bool requires)
        {
            // Loops through prfile given for tutoring to find matches 
            List<Match> matches = new List<Match>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            for (int i = 0; i < courses.Count; i++)
            {
                // Finds users who have same courses and individal selections but opposite information for requries
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
                        string matchName = GetName(id);
                        string overlap = ScheduleComparing.GetOverlap(username, id);
                        Match newMatch = new Match(matchName, courses[i], overlap);
                        matches.Add(newMatch);
                        int countMatchExists = MatchExists(username, id, courses[i]);
                        // If match does not alread exist, enter into database
                        if (countMatchExists == 0)
                        {
                            //string overlap = ScheduleComparing.GetOverlap(username, id);
                            InsertMatch(username, id, courses[i], overlap);
                        }
                    }
                }
                catch (Exception e) { return matches; }
                dr.Close();
            }
            
            conn.Close();
            return matches;
        }

        // SQL to get all the matches for a single user  
        public List<Match> DisplayMatches(string username)
        { 
            // Selects all matches for a single user 
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

                // Adds user to list of matches
                string matchName = GetName(matchId);
                Match newMatch = new Match(matchName, reason, overlap);
                matches.Add(newMatch);

            }
            dr.Close();
            conn.Close();
            return matches;
        }

        // SQL to get which activities a user selected, to know what to match for 
        public (string, string, string, string, string) GetActivityProfile(string username)
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
            // Returns all the activities they have entered 
            return (activity1, activity2, activity3, activity4, activity5);
        }

        // SQL to get what infromation is in a users tutoring profile, to know what to match for 
        public (string, string, string, string, string, string, bool, bool) GetTutoringProfile(string username)
        {
            List<string> courses = new List<string>();
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
            // Returns all the information in profile
            return (course1, course2, course3, course4, course5, course6, individual, requires);

        }

        // SQL to insert a match into the matches table of database
        public bool InsertMatch(string username, int matchId, string reason, string overlap)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Matches (userId, matchId, reason, overlap) VALUES ((SELECT id FROM UserAccounts WHERE UserAccounts.username = @username), @matchId, @reason, @overlap)", conn);
            cmd.Parameters.AddWithValue("@matchId", matchId);
            cmd.Parameters.AddWithValue("@reason", reason);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@overlap", overlap);
            // Success
            try
            {
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            // Fail
            catch
            {
                conn.Close();   
                return false;
            }

        }
        // SQL to see if a match already exists in the database, as to not enter any duplicates or cause any foreign key errors
        // If row already exists, returns 1, else reutrns 0
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
            conn.Close();
            return count;
        }

        // Gets the name of a user from a match 
        public string GetName(int id)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT username FROM UserAccounts WHERE UserAccounts.id = @id ", conn);
            cmd.Parameters.AddWithValue("@id", id);
            string username = (string)cmd.ExecuteScalar();
            return username;
        }

        // SQL to see if a user is opted in or not in order to be considered in matching logic 
        public bool CheckOptedIn(string username)
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


        // SQL to change a users opt in/out status 
        public bool UpdateOptStatus(string username, bool opt)
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


        // SQL to get a users schedule
        public bool GetSchedule(string username, int scheduleId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT schedule FROM Schedules WHERE Schedules.userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND Schedules.scheduleId = @scheduleId ", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@scheduleId", scheduleId);
            cmd.ExecuteScalar();
            return true;
        }


        // SQL to compare schedules
        public string CompareSchedule(int scheduleAId, int scheduleBId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT overlap FROM ScheduleCommpare WHERE ScheduleCompare.scheduleOne = @scheduleAId AND ScheduleCompare.scheduleTwo = @schedulBId ", conn);
            cmd.Parameters.AddWithValue("@scheduleAId", scheduleAId);
            cmd.Parameters.AddWithValue("@scheduleBId", scheduleBId);
            string overlap = (string) cmd.ExecuteScalar();
            return overlap;
        }
    }
}
