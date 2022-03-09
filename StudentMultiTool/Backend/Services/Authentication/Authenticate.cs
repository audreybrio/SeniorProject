using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Authentication
{
    public class Email
    {
        public static void SendEmail(string email, string otp)
        {
            String from = "studentmultitool@outlook.com";
            String subject = "OTP code for Student Multi-Tool";
            String msg = otp;
            String to = email;
            MailMessage mail = new MailMessage(from, to, subject, msg);
            SmtpClient client = new SmtpClient("email-smtp.us-east-1.amazonaws.com");
            client.Port = 25;
            client.Credentials = new System.Net.NetworkCredential("AKIA4LFTDFRCSQHGW2BL", "BMAUAXuLN+qSGL0QiezLwtqpfckzibBAwvJ/0AiDtrQa"); //change username and password to your email account username and password
            client.EnableSsl = true;
            client.Send(mail);
        }
    }

    public class Log
    {
        public static void LogIP(string email)
        {
            string myIP = getIP();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand c = new SqlCommand("SELECT id FROM UserAccounts WHERE UserAccounts.email = @email", conn);
            c.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = c.ExecuteReader();
            int id = 0;
            reader.Close();
            id = (int)c.ExecuteScalar();
            DateTime timeStamp = DateTime.Now;


            SqlCommand cmd = new SqlCommand("INSERT INTO Logs (timestamp, layer, category, userID, description) VALUES (@timeStamp, @layer, @category, @userID, @description)", conn);
            cmd.Parameters.AddWithValue("@timeStamp", timeStamp);
            cmd.Parameters.AddWithValue("@layer", "Security");
            cmd.Parameters.AddWithValue("@category", "Error");
            cmd.Parameters.AddWithValue("@userID", id);
            cmd.Parameters.AddWithValue("@description", "Invalid login attempt at " + myIP);
            cmd.ExecuteNonQuery();


        }

        public static string getIP()
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[1].ToString();
            return myIP;
        }
    }


    public class Otp
    {
        public static string Randomize(string email)
        {
            Random rand = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            string otp = new string(Enumerable.Repeat(chars, 8).Select(s => s[rand.Next(s.Length)]).ToArray());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();

            SqlCommand c = new SqlCommand("SELECT id FROM UserAccounts WHERE UserAccounts.email = @email", conn);
            c.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = c.ExecuteReader();
            int id = 0;
            reader.Close();
            id = (int)c.ExecuteScalar();
            DateTime timeStamp = DateTime.Now;

            SqlCommand cmd = new SqlCommand("INSERT INTO OTP (timestamp, otp, userID) VALUES (@timestamp, @otp, @userID)", conn);
            cmd.Parameters.AddWithValue("@timestamp", timeStamp);
            cmd.Parameters.AddWithValue("@otp", otp);
            cmd.Parameters.AddWithValue("@userID", id);
            cmd.ExecuteNonQuery();
            return otp;


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
            SqlCommand cmd = new SqlCommand("SELECT COUNT (username)" + " from UserAccounts " + "WHERE UserAccounts.email = @email AND UserAccounts.passcode = @passcode", conn);
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
                    Console.WriteLine("ERROR: Account Disabled");
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
            SqlCommand cmd = new SqlCommand("SELECT active_status" + " from UserAccounts " + "WHERE UserAccounts.email = @email", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();
            bool active_status = (bool)cmd.ExecuteScalar();
            return active_status;
        }

        // Checks username and OTP
        public static int LoginUser(string username, string password)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();



            SqlCommand cmd = new SqlCommand("SELECT COUNT (otp)" + " from OTP WHERE " + "OTP.userID = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND OTP.otp = @password", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            reader.Close();
            count = (int)cmd.ExecuteScalar();
            return count;

        }


        public static int ValidTime(string email)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();

            SqlCommand c = new SqlCommand("SELECT id FROM UserAccounts WHERE UserAccounts.email = @email", conn);
            c.Parameters.AddWithValue("@email", email);
            SqlDataReader read = c.ExecuteReader();
            int id = 0;
            read.Close();
            id = (int)c.ExecuteScalar();

            SqlCommand cds = new SqlCommand("SELECT timestamp FROM Otp WHERE Otp.userId = @userId", conn);
            cds.Parameters.AddWithValue("@userId", id);
            cds.ExecuteNonQuery();
            DateTime time = (DateTime)cds.ExecuteScalar();

            DateTime localTime = DateTime.Now;

            DateTime validTime = time.AddHours(24);

            int compare = (validTime.CompareTo(localTime));
            return compare;
        }



    }

    public class UserManagement
    {
        public static void UpdateDisable(string email)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserAccounts" + " SET active_status = @newStatus" + " WHERE email = @email", conn);
            cmd.Parameters.AddWithValue("@newStatus", 0);
            cmd.Parameters.AddWithValue("@email", email);
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


            Console.WriteLine("Welcome to Student Multi-Tool, Please log in.");
            Console.WriteLine("Enter Email.");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Passphrase");
            string passcode = Console.ReadLine();
            bool log;
            log = Validate.UserExist(email, passcode);

            if (log == true)
            {

                string otp = Otp.Randomize(email);
                Email.SendEmail(email, otp);



                int attempts = 1;

                while (attempts < 6)
                {


                    Console.WriteLine("Enter Username");
                    string username = Console.ReadLine();

                    Console.WriteLine("Enter OTP");
                    string password = Console.ReadLine();
                    int count = Validate.LoginUser(username, password);
                    int compare = Validate.ValidTime(email);
                    if (compare >= 0)
                    {

                        if (count > 0)
                        {
                            Console.Write("Login Success");
                            string ip = Log.getIP();

                            break;
                        }
                        else
                        {
                            Console.WriteLine("Login Incorrect. Try again.");
                            // Log ip address
                            Log.LogIP(email);
                            attempts++;
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid OTP");
                        break;
                    }
                }

                if (attempts >= 6)
                {
                    Console.WriteLine("Too many incorrect attempts. Account Disabled");
                    UserManagement.UpdateDisable(email);
                }
            }
            else
            {
                Console.WriteLine("Incorrect Email or Passcode");
            }


        }



    }

}




