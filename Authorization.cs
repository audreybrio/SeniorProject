using System;
using System.Data.SqlClient;

namespace Authorization
{

    class authorize
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

        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            string userName = "abrio";
            SqlCommand cmd = new SqlCommand("SELECT COUNT(username)" + " from UserTable" + " WHERE UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@username", userName);
            cmd.ExecuteNonQuery();
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                string userRole = getUserRole(userName);
                Console.WriteLine(userRole);
                if (userRole == "admin")
                {
                    Console.WriteLine("User is admin");
                }
                if (userRole == "student")
                {
                    Console.WriteLine("User is student");
                }
            }
            else
            {
                Console.WriteLine("Error: Current user is an unauthorized user.");
            }

        }
    }
}