using System;
using System.Data.SqlClient;

namespace UserManagement
{
    public class UserManager
    {
        public static bool DeleteUser(string username)
        {
            bool userExist = Validate.userExist(username);
            if (userExist == true)
            {
                int temp;
                temp = Update.UpdateDelete(username);
                bool t = Evaluate.Eval(temp);
                if (t == true)
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
    }

    public class Authorize
    {
        static bool authorize(string username)
        {
            //authorize
            return true;
        }
    }
    public class Validate
    {
        public static bool userExist(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd3 = new SqlCommand("SELECT COUNT (username)" + " from UserTable " + "WHERE UserTable.username = @username", conn);
            cmd3.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd3.ExecuteReader();
            int count = 1;
            reader.Close();
            count = (int)cmd3.ExecuteScalar();
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

    public class Update
    {
        public static int UpdateDelete(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM UserTable WHERE UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT (username)" + " from UserTable " + "WHERE UserTable.username = @username", conn);
            cmd2.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd2.ExecuteReader();
            int count = 1;
            reader.Close();
            count = (int)cmd2.ExecuteScalar();
            return count;
        }
    }

    public class Evaluate
    {
        public static bool Eval(int rowsAffected)
        {
            if (rowsAffected == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string Eval(bool affected)
        {
            if (affected == true)
            {
                string a = "successfully deleted.";
                return a;
            }
            else
            {
                string a = "Error: ";
                return a;
            }
        }


        static void Main(string[] args)
        {

            
        }
    }
}
