using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Rextester
{
    public class Program
    {

        public static bool updatetUserRole(string username, string role)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT (username)" + " from UserTable" + " WHERE UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                bool userExists = userRole(username, role);
                if (userExists == true)
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

        public static bool userRole(string username, string role)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT (username)" + " from UserTable" + " WHERE UserTable.username = @username AND UserTable.role = @role", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@role", role);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            int count = (int)cmd.ExecuteScalar();
            if (count == 0)
            {
                updateRole(username, role);
                Console.WriteLine("update successful");
                return true;
            }
            else
            {
                Console.WriteLine("update failed.");
                return false;
            }
        }

        public static void updateRole(string username, string newRole)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserTable" + " SET role = @role" + " WHERE username = @username", conn);
            cmd.Parameters.AddWithValue("@role", newRole);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
        }

        // Main 
        public static void Main(string[] args)
        {
            string username = "mkriesel";
            string role = "student";
            bool temp = updatetUserRole(username, role);
            Console.WriteLine(temp);

        }
    }
}
