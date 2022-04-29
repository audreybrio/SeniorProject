using StudentMultiTool.Backend.Services.Matching;
using System.Data.SqlClient;
using StudentMultiTool.Backend.Services;

namespace StudentMultiTool.Backend.DAL
{
    public class ActivityDAL
    {
        const string connectionString = "MARVELCONNECTIONSTRING";

        public static bool ActivityProfileUpdate(string activity1, string activity2, string activity3, string activity4, string activity5, string username)
        {
            //string username = "abrio";
            // bool opt = true;

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
            conn.Close();
            return true;

 
        }

        public static bool ActivityProfileInsert(string activity1, string activity2, string activity3, string activity4, string activity5, string username, bool opt){
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
            conn.Close ();
            return true;
        }

        public static int ProfileExists(string username)
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
            conn.Close();
            return count;
        }
    }
}
