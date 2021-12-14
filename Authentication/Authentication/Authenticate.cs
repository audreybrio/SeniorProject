using System;
using System.Data.SqlClient;

namespace Authentication
{
    class Authenticate
    {
        public static bool Authen(string email, string passcode)
        {
            bool exists;
            exists = Validate.UserExist(email, passcode);
            if (exists == false)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }


    class Validate
    {
        public static bool UserExist(string email, string passcode)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT (username)" + " from UserTable " + "WHERE UserTable.email = @email AND UserTable.passcode = @passcode", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@passcode", passcode);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            reader.Close();
            count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int loginUser(string username, string password)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT (username)" + " from UserTable " + "WHERE UserTable.username = @username AND UserTable.password = @password", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            reader.Close();
            count = (int)cmd.ExecuteScalar();
            return count;

        }

    }


    class Evaluate
    {


        public static bool Eval(int rowsAffected)
        {
            if (rowsAffected > 0)
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
                string a = "User Logged in Successfully.";
                return a;
            }
            else
            {
                string a = "Error. Username/Password Incorrect.";
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
            cmd.Parameters.AddWithValue("@name", "Audrey Brio");
            cmd.Parameters.AddWithValue("@username", "abrio");
            cmd.Parameters.AddWithValue("@password", "Pas$word1");
            cmd.Parameters.AddWithValue("@email", "audrey.brio@student.csulb.edu");
            cmd.Parameters.AddWithValue("@passcode", "1234");
            cmd.Parameters.AddWithValue("@role", "admin");
            cmd.Parameters.AddWithValue("@active_status", 1);
            SqlCommand cmd2 = new SqlCommand("Insert into UserTable" + "(id, name, username, password, email, passcode, role, active_status) " + "values (@id, @name, @username, @password, @email, @passcode, @role, @active_status)", conn);
            cmd2.Parameters.AddWithValue("@id", 2);
            cmd2.Parameters.AddWithValue("@name", "Bradly Nickle");
            cmd2.Parameters.AddWithValue("@username", "bnickle");
            cmd2.Parameters.AddWithValue("@password", "Pas$word2");
            cmd2.Parameters.AddWithValue("@email", "bradly.nickle@student.csulb.edu");
            cmd2.Parameters.AddWithValue("@passcode", "12345");
            cmd2.Parameters.AddWithValue("@role", "student");
            cmd2.Parameters.AddWithValue("@active_status", 1);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            Console.WriteLine("Enter email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter passcode: ");
            string passcode = Console.ReadLine();
            bool log;
            log = Authenticate.Authen(email, passcode);
            if (log == true)
            {
                Console.WriteLine("Passcode correct, sending password to email.");
                int count = 1;
                while (count <= 5)
                {
                    Console.WriteLine("Enter username: ");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter password: ");
                    string password = Console.ReadLine();
                    int temp;
                    temp = Validate.loginUser(username, password);
                    bool t = Eval(temp);
                    if (t == true)
                    {
                        string a = Eval(t);
                        Console.WriteLine($"{a}");
                        break;
                    }
                    else
                    {
                        string a = Eval(t);
                        Console.WriteLine($"{a}");
                        count++;
                    }
                }
                if (count > 5)
                {
                    Console.WriteLine("Error: User attempted more than 5 tries. Account is disbled.");
                }
            }
            else
            {
                Console.WriteLine("Email/Passcode Incorrect.");
            }

        }
    }
}
