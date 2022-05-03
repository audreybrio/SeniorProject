using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using StudentMultiTool.Backend.Services.Authentication;
using System.Net.Mail;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/" + "login")]
    public class LoginController : Controller
    {
        const string connectionString = "MARVELCONNECTIONSTRING";
        private int attempts = 0;
        //AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal); 
        //WindowsPrincipal myPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;

        [HttpGet("getlogin")]
        public IActionResult Login()
        {
            return Ok("okay");
        }

        [HttpPost("validate/{email}/{passcode}")]
        public IActionResult EmailPasscodeCheck(string email, string passcode)
        { 
            bool valPasscode, valEmail, doesExist;
            Validate val = new Validate();
            valPasscode = val.ValidatePasscode(passcode);
            valEmail = val.ValidateEmail(email);

            if (valPasscode && valEmail)
            {
                doesExist = UserExist(email, passcode);
                if (doesExist == true)
                {
                    string otp = Randomize(email);
                    SendEmail(email, otp);
                    return Ok();
                }

                else
                {
                    return NotFound(); 
                }   
            }
            else
            {
                return NotFound() ;
            }
      
        }

        [HttpPost("authenticate/{username}/{otp}")]
        public IActionResult AuthenticateUser(string username, string otp)
        {
            attempts++;
            Console.WriteLine(attempts);
            while (attempts < 6)
            {
                int count = LoginUser(username, otp);
                //bool isValid = ValidTime(username);
                //if (!isValid)
                //{
                //    return NotFound();
                //}

                if (count > 0)
                {
                    attempts = 0;
                    return Ok();
                }
                else
                {
                    LogIP log = new LogIP();
                    log.LoggingIP(username);
                    return NotFound();

                }
            }
            UpdateDisable(username);
            attempts = 0;
            return NotFound() ;

        }
        //public IActionResult Authenticate()
        //{

        //    Console.WriteLine("Welcome to Student Multi-Tool, Please log in.");
        //    Console.WriteLine("Enter Email.");
        //    string email = Console.ReadLine();
        //    Console.WriteLine("Enter Passphrase");
        //    string passcode = Console.ReadLine();

        //    bool doesExist, valPasscode, valEmail;
        //    Validate val = new Validate();
        //    valPasscode = val.ValidatePasscode(passcode);
        //    valEmail = val.ValidateEmail(email);

        //    if (valPasscode && valEmail)
        //    {
        //        doesExist = UserExist(email, passcode);
        //        // If user exists
        //        if (doesExist == true)
        //        {
        //            // Get otp and send it
        //            string otp = Randomize(email);
        //            //SendEmail(email, otp);

        //            int attempts = 1;
        //            // User has 5 attempts to log in 
        //            while (attempts < 6)
        //            {

        //                Console.WriteLine("Enter Username");
        //                string username = Console.ReadLine();

        //                Console.WriteLine("Enter OTP");
        //                string password = Console.ReadLine();
        //                int count = LoginUser(username, password);
        //                bool isValid = ValidTime(email);
        //                if (!isValid)
        //                {
        //                    Console.WriteLine("Invalid OTP");
        //                    break;
        //                }
        //                if (count > 0)
        //                {
        //                    Console.Write("Login Success");
        //                    // Changed out for redirct to homepage
        //                    Logout();

        //                    break;
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Login Incorrect. Try again.");
        //                    // Log ip address
        //                    LogIP log = new LogIP();
        //                    log.LoggingIP(email);
        //                    attempts++;
        //                    continue;
        //                }

        //            }

        //            if (attempts >= 6)
        //            {
        //                Console.WriteLine("Too many incorrect attempts. Account Disabled");
        //                UpdateDisable(email);
        //            }
        //        }

        //        else
        //        {
        //            Console.WriteLine("Incorrect Email or Passcode");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Not Valid Email or Passcode");
        //    }
        //    return new OkResult();
        //}

        // Checks if user exists in database
        public bool UserExist(string email, string passcode)
        {
            string passphrase = HashPass(passcode);
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
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
                // Checks is user is disabled
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

        // Hashes passcode
        public string HashPass(string password)
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

        // Generates otp and inserts into db
        public string Randomize(string email)
        {
            Random rand = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            string otp = new string(Enumerable.Repeat(chars, 8).Select(s => s[rand.Next(s.Length)]).ToArray());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
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

        // Checks is user is disabled
        public bool CheckDisabled(string email)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT active_status" + " from UserAccounts " + "WHERE UserAccounts.email = @email", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();
            bool active_status = (bool)cmd.ExecuteScalar();
            return active_status;
        }

        // Sends otp via email
        public  void SendEmail(string email, string otp)
        {
            String from = "studentmultitool@outlook.com";
            String subject = "OTP code for Student Multi-Tool";
            String msg = otp;
            String to = email;
            MailMessage mail = new MailMessage(from, to, subject, msg);
            SmtpClient client = new SmtpClient("email-smtp.us-east-1.amazonaws.com");
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("AKIA4LFTDFRCSQHGW2BL", "BMAUAXuLN+qSGL0QiezLwtqpfckzibBAwvJ/0AiDtrQa"); 
            client.EnableSsl = true;
            client.Send(mail);
        }









        // Validates user entered correct credentials to enter system
        public static int LoginUser(string username, string password)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
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

        // Checks is OTP is still valid
        public static bool ValidTime(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();

            SqlCommand c = new SqlCommand("SELECT id FROM UserAccounts WHERE UserAccounts.username = @username", conn);
            c.Parameters.AddWithValue("@username", username);
            SqlDataReader read = c.ExecuteReader();
            int id = 0;
            read.Close();
            if (c.ExecuteScalar() == null)
            {
                return false;
            }
           id = (int)c.ExecuteScalar();

            SqlCommand cds = new SqlCommand("SELECT timestamp FROM Otp WHERE Otp.userId = @userId", conn);
            cds.Parameters.AddWithValue("@userId", id);
            cds.ExecuteNonQuery();
            DateTime time = (DateTime)cds.ExecuteScalar();

            DateTime localTime = DateTime.Now;

            DateTime validTime = time.AddHours(24);

            int compare = (validTime.CompareTo(localTime));
            if (compare >= 0)
            {
                return true;
            }
            else { return false; }
        }


        // Disables a user if they exceed 5 incorrect login attempts
        public void UpdateDisable(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserAccounts SET active_status = @newStatus WHERE username = @username", conn);
            cmd.Parameters.AddWithValue("@newStatus", 0);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
        }



    }
}
