using StudentMultiTool.Backend.Services.Matching;
using System.Data.SqlClient;
using StudentMultiTool.Backend.Services;


namespace StudentMultiTool.Backend.DAL
{
    // Tutorin DAL
    public class TutoringDAL
    {
        // Connection string
        const string connectionString = "MARVELCONNECTIONSTRING";

        // SQL to update tutoring profile information
        public static bool TutoringProfileUpdate(string course1, string course2, string course3, string course4, string course5, string course6, string username, bool individual, bool requires)
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
            conn.Close();
            return true;
        }

        // SQL to insert tutoring profile information
        public static bool TutoringProfileInsert(string course1, string course2, string course3, string course4, string course5, string course6, string username, bool individual, bool requires, bool opt)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO TutoringProfile (userId, course1, course2, course3, course4, course5, course6, individual, requires, opt) VALUES ( (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username), @course1, @course2, @course3, @course4, @course5, @course6, @individual, @requires, @opt)", conn);
            cmd.Parameters.AddWithValue("@course1", course1);
            cmd.Parameters.AddWithValue("@course2", course2);
            cmd.Parameters.AddWithValue("@course3", course3);
            cmd.Parameters.AddWithValue("@course4", course4);
            cmd.Parameters.AddWithValue("@course5", course5);
            cmd.Parameters.AddWithValue("@course6", course6);
            cmd.Parameters.AddWithValue("@individual", individual);
            cmd.Parameters.AddWithValue("@requires", requires);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@opt", opt);
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }


        // SQL to check if a user profile exists in database
        public static int ProfileExists(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT (userId) FROM TutoringProfile WHERE userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username)", conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd.ExecuteReader();
            int countProfile = 0;
            reader.Close();
            countProfile = (int)cmd.ExecuteScalar();
            conn.Close();
            return countProfile;
        }
    }
}
