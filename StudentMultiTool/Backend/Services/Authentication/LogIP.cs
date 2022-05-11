using System.Net;
using System.Data.SqlClient;
namespace StudentMultiTool.Backend.Services.Authentication
{
    public class LogIP
    {
        // Gets ip address of user
        const string connectionString = "MARVELLOGCONNECTIONSTRING";
        public string GetIP()
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[1].ToString();
            return myIP;
        }

        public void LoggingIP(string username)
        {
            string myIP = GetIP();

            int userExists = UserExists(username);
            if(userExists != 0)
            {

                int id = GetUserId(username);
                DateTime timeStamp = DateTime.Now;
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Logs (timestamp, layer, category, userID, description) VALUES (@timeStamp, @layer, @category, @userID, @description)", conn);
                cmd.Parameters.AddWithValue("@timeStamp", timeStamp);
                cmd.Parameters.AddWithValue("@layer", "Security");
                cmd.Parameters.AddWithValue("@category", "Error");
                cmd.Parameters.AddWithValue("@userID", id);
                cmd.Parameters.AddWithValue("@description", "Invalid login attempt at " + myIP);
                cmd.ExecuteNonQuery();
            }



        }


        public int UserExists(string username)
        {

            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand c = new SqlCommand("SELECT COUNT(id) FROM UserAccounts WHERE UserAccounts.username = @username", conn);
                c.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = c.ExecuteReader();
                int id = 0;
                reader.Close();
                id = (int)c.ExecuteScalar();
               return id;
            }
            catch
            {
                return 0;
            }

        }

        public int GetUserId(string username)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand c = new SqlCommand("SELECT id FROM UserAccounts WHERE UserAccounts.username = @username", conn);
                c.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = c.ExecuteReader();
                int id = 0;
                reader.Close();
                id = (int)c.ExecuteScalar();
                return id;
            }
            catch
            {
                return 10;
            }

        }

        

    }
}
