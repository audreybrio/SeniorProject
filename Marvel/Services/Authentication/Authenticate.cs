using System;
using System.Data.SqlClient;

// Authenticate a user to log into the system 
namespace Authentication
{

    public class Email
    {
        public static void sendEmail(string email, string otp)
        {
            String from = "studentmultitool@outlook.com"; // change to your email address for the application
            String subject = "OTP code for yourApp"; //rename yourApp to your application
            String msg = otp;
            String to = "success@simulator.amazonses.com";
            MailMessage mail = new MailMessage(from, to, subject, msg);
            SmtpClient client = new SmtpClient("email-smtp.us-east-1.amazonaws.com"); //set to your outgoing smtp server is gmail
            client.Port = 25; //smtp port for SSL
            client.Credentials = new System.Net.NetworkCredential("AKIA4LFTDFRCSQHGW2BL", "BMAUAXuLN+qSGL0QiezLwtqpfckzibBAwvJ/0AiDtrQa"); //change username and password to your email account username and password
            client.EnableSsl = true; //for gmail SSL must be true
            client.Send(mail);
        }
    }


    public class Otp
    {
        public static string Randomize()
        {
            Random rand = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[rand.Next(s.Length)]).ToArray());

        }
    }

    public class Hash
    {
        public static string HashPass(string password)
        {


            string s = "teammarvel";
            byte[] salt = Encoding.ASCII.GetBytes(s);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;


        }
    }






    public class Validate
    {
        // Checks users email and pass phrase 
        public static bool UserExist(string email, string passcode)
        {
            string passphrase = Hash.HashPass(passcode);


            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT (username)" + " from UserTable " + "WHERE UserTable.email = @email AND UserTable.passcode = @passcode", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@passcode", passphrase);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            reader.Close();
            count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                bool isDiasbled = CheckDisabled(email);
                if (isDiasbled == true)
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

        // Checks is user is diasabled 
        public static bool CheckDisabled(string email)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT active_status" + " from UserTable " + "WHERE UserTable.email = @email", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();
            bool active_status = (bool)cmd.ExecuteScalar();
            return active_status;
        }

        // Checks username and OTP
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

    public class UserManagement
    {
        public static void UpdateDisable(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserTable" + " SET active_status = @newStatus" + " WHERE username = @username", conn);
            cmd.Parameters.AddWithValue("@newStatus", 0);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
        }
    }




    public partial class Login
    {

        private void check(object sender, EventArgs e)
        {

            string email;
            string passphrase;

        }
    }

    // Fix to start using 
    public class Evaluate
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
            string email = "audrey.brio@student.csulb.edu";
            string passcode = "hello world";
            bool log;
            log = Validate.UserExist(email, passcode);
            Console.WriteLine(log);

            if (log == true)
            {
                int attempts = 1;
                string username = "abrio";

                while (attempts < 6)
                {
                    string otp = Otp.Randomize();
                    Console.WriteLine(otp);

                    //Email.sendEmail(email, otp);
                    // send email 

                    // start cookie 


                    Console.WriteLine("Enter Password");
                    string password = Console.ReadLine();
                    int count = Validate.loginUser(username, password);
                    if (count > 0)
                    {
                        Console.Write("Login Success");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Login Incorrect. Try again.");
                        // log ip address

                        attempts++;
                    }
                }

                if (attempts >= 6)
                {
                    UserManagement.UpdateDisable(username);
                }
            }


        }



    }
}
