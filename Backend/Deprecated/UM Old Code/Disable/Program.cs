using System;
using System.Data.SqlClient;

namespace Disable
{
    public class DisableUser
    {

        public static string getUserRole(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT role" + " from UserTable " + "WHERE UserTable.role = role", conn);
            cmd.ExecuteNonQuery();
            string role = "";
            role = Convert.ToString(cmd.ExecuteScalar());
            return role;
        }

        public static bool userExist(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(username)" + " from UserTable" + " WHERE UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                bool isEnabled = isEnable(username);
                if (isEnabled == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }

        public static bool isEnable(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT active_status" + " from UserTable" + " WHERE UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            bool active_status = (bool) cmd.ExecuteScalar();
            if (active_status == true)
            {
                userDisable(username);
                return true;
            }
            else if (active_status == false)
            {
                return false;
            }
            else
            {
                Console.WriteLine("Error: active status not found");
                return false;
            }
        }

        public static void userDisable(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserTable" + " SET active_status = @newStatus" + " WHERE username = @username", conn);
            cmd.Parameters.AddWithValue("@newStatus", 0);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
        }

        // Main 
        static void Main(string[] args)
        {

            
        }
    }
}
