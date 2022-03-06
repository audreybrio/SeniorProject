using StudentMultiTool.Backend.Services.Authentication.Model;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Threading.Tasks;
using System.Web;
using System.Text;

namespace StudentMultiTool.Backend.Services.Authentication.Controller
{
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public IActionResult Login()
        {
            return new OkResult();
        }
        public ActionResult Authorize(User user)
        {

            string email = user.Email;
            string passcode = user.Passcode;






            string s = "teammarvel";
            byte[] salt = Encoding.ASCII.GetBytes(s);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passcode,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            Console.WriteLine(hashed);




            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT (username)" + " from UserTable " + "WHERE UserTable.email = @email AND UserTable.passcode = @passcode", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@passcode", hashed);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            reader.Close();
            count = (int)cmd.ExecuteScalar();




            if (count > 0)
            {
                bool isDisabled = CheckDisabled(email);
                if (isDisabled == true)
                {
                    string pass = Randomize(email);
                    sendEmail(email, pass);
                    return RedirectToAction("Index", "Log");
                }
                else
                {
                    user.Login3ErrorMsg = "Error: Account Disabled";
                    return RedirectToAction("Login", user);
                }

            }

            else
            {

                user.LoginErrorMsg = "Error: Invalid Email or Passcode";
                return RedirectToAction("Index", user);

            }









        }

        public string Randomize(string email)
        {
            Random rand = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            string otp = new string(Enumerable.Repeat(chars, 8).Select(s => s[rand.Next(s.Length)]).ToArray());
            //Console.WriteLine(otp);

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserTable SET password = @password WHERE email = @email", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", otp);
            cmd.ExecuteNonQuery();
            return otp;
        }

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


        public static void sendEmail(string email, string otp)
        {
            String from = "studentmultitool@outlook.com"; // change to your email address for the application
            String subject = "OTP code for Student Multi-Tool"; //rename yourApp to your application
            String msg = otp;
            String to = email;
            MailMessage mail = new MailMessage(from, to, subject, msg);
            SmtpClient client = new SmtpClient("email-smtp.us-east-1.amazonaws.com"); //set to your outgoing smtp server is gmail
            client.Port = 25; //smtp port for SSL
            client.Credentials = new System.Net.NetworkCredential("AKIA4LFTDFRCSQHGW2BL", "BMAUAXuLN+qSGL0QiezLwtqpfckzibBAwvJ/0AiDtrQa"); //change username and password to your email account username and password
            client.EnableSsl = true; //for gmail SSL must be true
            client.Send(mail);
        }


    }
}
