using System;
using System.Data.SqlClient;

namespace CreateUser
{

    class Program
    {
        public static bool CreateUsers(string username, string password, string email)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO UserTable " + "(username, password, email, role, active_status) " + "values (@username, @password, @email)", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@role", "student");
            cmd.ExecuteNonQuery();
            int rowCount = 1;
            reader.Close();
            rowCount = (int)cmd.ExecuteScalar();

            if (rowCount == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            string[] username1 = { "", "", "", "", ""};
            foreach (string username2 in username1)
            {
                bool b = ValidateUserName(username);

            }
            string[] password1 = { "", "", "", "", "", "", "", "" };
            foreach (string password2 in password1)
            {
                bool b = ValidatePassword(password);
            }
            string[] email1 = email;


            static bool ValidateUserName(string username)
            {
                int validCondition = 0;
                foreach (char c in username)
                {
                    if (c >= 'a' && c <= 'z')
                    {
                        validCondition++;
                        break;
                    }
                }
                if (validCondition == 0)
                    return false;
                foreach (char c in username)
                {
                    if (c >= '0' && c <= '9')
                    {
                        validCondition++;
                        break;
                    }
                }
                if (validCondition == 1)
                    return false;
                if (validCondition == 2)
                {
                    char[] special = { '.', ',', '@', '!' };
                    if (username.IndexOfAny(special) == -1)
                        return false;
                }
                return true;
            }

            static bool ValidatePassword(string password)
            {
                int validCondition = 0;
                foreach (char c in password)
                {
                    if (c >= 'a' && c <= 'z')
                    {
                        validCondition++;
                        break;
                    }
                }
                foreach (char c in password)
                {
                    if (c >= 'A' && c <= 'Z')
                    {
                        validCondition++;
                        break;
                    }
                }
                if (validCondition == 0)
                    return false;
                foreach (char c in password)
                {
                    if (c >= '0' && c <= '9')
                    {
                        validCondition++;
                        break;
                    }
                }
                if (validCondition == 1)
                    return false;
                if (validCondition == 2)
                {
                    char[] special = { '.', ',', '@', '!' };
                    if (password.IndexOfAny(special) == -1)
                        return false;
                }
                return true;
            }

            return true;
        }

        public static bool UserExist(string email, string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT (username)" + " from UserTable " + "WHERE UserTable.email = @email AND UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd.ExecuteReader();
            int rowCount = 1;
            reader.Close();
            rowCount = (int)cmd.ExecuteScalar();

            if (rowCount == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static long generateID()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT MAX(id) AS ID" + " FROM UserTable", conn);
            cmd.ExecuteNonQuery();
            long id = (long)cmd.ExecuteScalar();
            if (id != NULL)
            {
                return id+1;
            }
            else
            {
                return 1;
            }
        }

        public static int UpdateCreate(string name, string username, string password, string email, string passcode)
        {
            // inserts the created user into the database
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            long id = generateID();
            SqlCommand cmd = new SqlCommand("INSERT INTO UserTable " + "(id, name, username, password, email, passcode, role, active_status) " + "values (@id, @name, @username, @password, @email, @passcode, @role, @active_status)", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@passcode", passcode);
            cmd.Parameters.AddWithValue("@role", "student");
            cmd.Parameters.AddWithValue("@active_status", 1);
            cmd.ExecuteNonQuery();
            return 1;
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"Do you want to create an account?");
            string confirm = Console.ReadLine();
            if (confirm.Equals("yes") || confirm.Equals("y") || confirm.Equals("Y") || confirm.Equals("Yes") || confirm.Equals("YES"))
            {
                Console.WriteLine($"Your Username must be a minimum of 5 characters, include: a-z, 0-9, and/or .,@! ");
                Console.WriteLine($"Your password must be a minimum of 8 characters, including: a-z, A-Z, and/or 0-9 ");
                Console.WriteLine($"Create a username: ");
                string username = Console.ReadLine();
                Console.WriteLine($"Create a password: ");
                string password = Console.ReadLine();
                foreach (string username1 in username && string password1 in password && string email1 in email)
                {
                    bool b = CreateUsers(username, password, email);
                }

            }
        }
    }
}