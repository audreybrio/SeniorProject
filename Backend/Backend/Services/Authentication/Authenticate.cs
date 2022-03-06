using System;
using System.Data.SqlClient;

// Authenticate a user to log into the system 
namespace Authentication
{

    // Takes in email and passcode and returns true if the credentials are correct 
     public class Authenticate
     {
        public static bool Authen(string email, string passcode)
        {
            bool userExists;
            userExists = Validate.UserExist(email, passcode);
            if (userExists == false)
            {
                return false;

            }
            else
            {
                return true;
            }

        }
    }

    // Validates user exists in the database
     public class Validate
     {
        public static bool UserExist(string email, string passcode)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();

            // deal with hashes


            SqlCommand cmd = new SqlCommand("SELECT COUNT (username)" + " from UserTable " + "WHERE UserTable.email = @email AND UserTable.passcode = @passcode AND UserTable.active_status = @active", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@passcode", passcode);
            cmd.Parameters.AddWithValue("active", true);
            SqlDataReader reader = cmd.ExecuteReader();
            int rowCount = 0;
            reader.Close();
            rowCount = (int)cmd.ExecuteScalar();

            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Validates user has correct username and password to log into the system
        public static int LoginUser(string username, string password)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            // deal with hashes


            SqlCommand cmd = new SqlCommand("SELECT COUNT (username)" + " from UserTable " + "WHERE UserTable.username = @username AND UserTable.password = @password", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader reader = cmd.ExecuteReader();
            int rowCount = 0;
            reader.Close();
            rowCount = (int)cmd.ExecuteScalar();
            return rowCount;

        }

    }

    // Evaluates the return types 
    public class Evaluate
    {

        // Turns int into a bool
        public static bool EvaluateBool(int rowsAffected)
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

        // Turns bool into string 
        public static string EvaluateString(bool rowAffected)
        {
            if (rowAffected == true)
            {
                string successCase = "User Logged in Successfully.";
                return successCase;
            }
            else
            {
                string failureCase = "Error. Username/Password Incorrect.";
                return failureCase;
            }
        }

        // Main 
        static void Main(string[] args)
        {
            string email = "michael.kriesel@student.csulb.edu";
            string passcode = "super man";
            bool log;
            log = Authenticate.Authen(email, passcode);

            string username = "mkriesel";
            string password = "Password1";
            int temp;
            temp = Validate.LoginUser(username, password);
            bool t = Evaluate.EvaluateBool(temp);
            Console.WriteLine(t);
        }
    }
}


