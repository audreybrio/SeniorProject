using System;
using System.Data.SqlClient;

namespace Enable
{
    public class EnableUser
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
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isDisable(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT active_status" + " from UserTable" + " WHERE UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            bool active_status = (bool) cmd.ExecuteScalar();
            if (active_status == false)
            {
                return true;
            }
            else if (active_status == true)
            {
                return false;
                
            }
            else
            {
                Console.WriteLine("Error: active status not found");
                return false;
            }
        }

        public static void userEnable(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserTable" + " SET active_status = @newStatus" + " WHERE username = @username", conn);
            cmd.Parameters.AddWithValue("@newStatus", 1);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
        }

        // Main 
        static void Main(string[] args)
        {
            string CurrentUsername = "abrio";
            if (getUserRole(CurrentUsername) == "admin")
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
                conn.Open();
                Console.WriteLine("Enter username to enable account: ");
                string userSelected = Console.ReadLine();
                if (userExist(userSelected) == true)
                {
                    if (isDisable(userSelected) == true)
                    {
                        userEnable(userSelected);
                    }
                }
                else
                {
                    Console.WriteLine("User status cannot be changed to DISABLE");
                }
            }
        }
    }
}
