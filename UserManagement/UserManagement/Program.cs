using System;
using System.Data.SqlClient;

namespace UserManagement
{
    class UserManager
    {
        public static bool DeleteUser(string username)
        {

            Console.WriteLine($"Are you sure you want to delete {username}?");
            string confirm = Console.ReadLine();
            if (confirm.Equals("yes") || confirm.Equals("y"))
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

    class Authorize
    {
        static bool authorize(string username)
        {
            //authorize
            return true;
        }
    }
    class Validate
    {

    }

    class Update
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

    class Evaluate
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
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into UserTable" + "(id, name, username, password, email, passcode, role, active_status) " + "values (@id, @name, @username, @password, @email, @passcode, @role, @active_status)", conn);
            cmd.Parameters.AddWithValue("@id", 1);
            cmd.Parameters.AddWithValue("@name", "Aud");
            cmd.Parameters.AddWithValue("@username", "abrio");
            cmd.Parameters.AddWithValue("@password", "Pas$word1");
            cmd.Parameters.AddWithValue("@email", "audo@student.csulb.edu");
            cmd.Parameters.AddWithValue("@passcode", "1234");
            cmd.Parameters.AddWithValue("@role", "admin");
            cmd.Parameters.AddWithValue("@active_status", 1);
            SqlCommand cmd2 = new SqlCommand("Insert into UserTable" + "(id, name, username, password, email, passcode, role, active_status) " + "values (@id, @name, @username, @password, @email, @passcode, @role, @active_status)", conn);
            cmd2.Parameters.AddWithValue("@id", 2);
            cmd2.Parameters.AddWithValue("@name", "Brad");
            cmd2.Parameters.AddWithValue("@username", "bnickle");
            cmd2.Parameters.AddWithValue("@password", "Pas$word2");
            cmd2.Parameters.AddWithValue("@email", "brad@student.csulb.edu");
            cmd2.Parameters.AddWithValue("@passcode", "12345");
            cmd2.Parameters.AddWithValue("@role", "student");
            cmd2.Parameters.AddWithValue("@active_status", 1);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            Console.WriteLine("Enter user to delete: ");
            string user = Console.ReadLine();

            SqlCommand cmd3 = new SqlCommand("SELECT COUNT (username)" + " from UserTable " + "WHERE UserTable.username = @username", conn);
            cmd3.Parameters.AddWithValue("@username", user);
            SqlDataReader reader = cmd3.ExecuteReader();
            int count = 1;
            reader.Close();
            count = (int)cmd3.ExecuteScalar();
            if (count == 0)
            {
                Console.WriteLine($"Error: {user} does not exist.");
            }
            else
            {
                bool t;
                t = UserManager.DeleteUser(user);
                if (t == true)
                {
                    string a = Eval(t);
                    Console.WriteLine($"{user} {a}");

                }
                else
                {
                    string a = Eval(t);
                    Console.WriteLine($"{a} {user} not deleted.");
                }
            }
        }
    }
}
