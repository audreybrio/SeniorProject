using System.Net;
using System.Data.SqlClient;
namespace StudentMultiTool.Backend.Services.Authentication
{
    public class LogIP
    {
        // Gets ip address of user
        public string GetIP()
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[1].ToString();
            return myIP;
        }

        public void LoggingIP(string email)
        {
            string myIP = GetIP();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand c = new SqlCommand("SELECT id FROM UserAccounts WHERE UserAccounts.email = @email", conn);
            c.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = c.ExecuteReader();
            int id = 0;
            reader.Close();
            id = (int)c.ExecuteScalar();
            DateTime timeStamp = DateTime.Now;


            SqlCommand cmd = new SqlCommand("INSERT INTO Logs (timestamp, layer, category, userID, description) VALUES (@timeStamp, @layer, @category, @userID, @description)", conn);
            cmd.Parameters.AddWithValue("@timeStamp", timeStamp);
            cmd.Parameters.AddWithValue("@layer", "Security");
            cmd.Parameters.AddWithValue("@category", "Error");
            cmd.Parameters.AddWithValue("@userID", id);
            cmd.Parameters.AddWithValue("@description", "Invalid login attempt at " + myIP);
            cmd.ExecuteNonQuery();


        }

    }
}
