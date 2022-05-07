using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using StudentMultiTool.Backend.Services.Authentication;
using System.Net.Mail;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/" + "login")]
    public class LoginController : Controller
    {
        const string connectionString = "MARVELCONNECTIONSTRING";
       // private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;

       /* public LoginController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }*/

        public LoginController()
        {
        }

        //private int attempts = 0;
        //AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal); 
        //WindowsPrincipal myPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;

        [HttpGet("getlogin")]
        public IActionResult Login()
        {
            return Ok("okay");
        }

        [HttpPost("validate")]
        public IActionResult EmailPasscodeCheck([FromBody] DataObj credientials)
        {
            string email = credientials.creditentials[0];
            string passcode = credientials.creditentials[1];
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

        [HttpPost("authenticate")]
        public IActionResult AuthenticateUser([FromBody] DataObj2 authen)
        {

            string username = authen.authen[0];
            string otp = authen.authen[1];
            int count = LoginUser(username, otp);

            if (count > 0)
            {
                string role = GetRole(username);
                var token = GenerateJwtToken(username, role);
                Console.WriteLine(token);
                return Ok(token);
            }
            else
            {
                LogIP log = new LogIP();
               // log.LoggingIP(username);
                return NotFound();

            }


        }


        [HttpGet]
        [Route("disable/{username}")]
        public IActionResult DisableUser(string username)
        {
            UpdateDisable(username);
            return Ok();
        }

        [HttpGet]
        [Route("getToken/{username}")]
        public string GetToken(string username)
        {
            string role = GetRole(username);
            var token = GenerateJwtToken(username, role);
            Console.WriteLine(token);
            return token;
        }



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
            Console.WriteLine(hashed);
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

            int countUser = UserExists(email);
            int userId = GetUserId(email);
            DateTime timeStamp = DateTime.Now;

            if (countUser == 0)
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO OTP (timestamp, otp, userID) VALUES (@timestamp, @otp, @userID)", conn);
                cmd.Parameters.AddWithValue("@timestamp", timeStamp);
                cmd.Parameters.AddWithValue("@otp", otp);
                cmd.Parameters.AddWithValue("@userID", userId);
                cmd.ExecuteNonQuery();
                return otp;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("UPDATE OTP SET timestamp = @timestamp, otp = @otp WHERE userId = @userID", conn);
                cmd.Parameters.AddWithValue("@timestamp", timeStamp);
                cmd.Parameters.AddWithValue("@otp", otp);
                cmd.Parameters.AddWithValue("@userID", userId);
                cmd.ExecuteNonQuery();
                return otp;
            }


        }

        public int UserExists(string email)
        {

            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand c = new SqlCommand("SELECT COUNT(id) FROM OTP WHERE OTP.userId = (SELECT id FROM UserAccounts WHERE UserAccounts.email = @email)", conn);
                c.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = c.ExecuteReader();
                int id = 0;
                reader.Close();
                id = (int)c.ExecuteScalar();
                return id;
            }
            catch
            {
                return 0;
            }

        }

        public int GetUserId(string email)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand c = new SqlCommand("SELECT id FROM UserAccounts WHERE UserAccounts.email = @email", conn);
                c.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = c.ExecuteReader();
                int id = 0;
                reader.Close();
                id = (int)c.ExecuteScalar();
                return id;
            }
            catch
            {
                return 10;
            }

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



            SqlCommand cmd = new SqlCommand("SELECT COUNT(otp)" + " from OTP WHERE " + "OTP.userID = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND OTP.otp = @password", conn);
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


        public string GetRole(string username)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand c = new SqlCommand("SELECT role FROM UserAccounts WHERE UserAccounts.username = @username", conn);
                c.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = c.ExecuteReader();
                string role = "";
                reader.Close();
                role = (string)c.ExecuteScalar();
                return role;
            }
            catch
            {
                return "student";
            }
        }


        private string GenerateJwtToken(string username, string role)
        {
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("TEAMMARVEL IS WORKING ON STUDENTMULTITOOL");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", username), new Claim("role", role) }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //Console.WriteLine(tokenHandler.WriteToken(token));
            return tokenHandler.WriteToken(token);
        }









        // Disables a user if they exceed 5 incorrect login attempts
        public void UpdateDisable(string username)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE UserAccounts SET active_status = @newStatus WHERE username = @username", conn);
                cmd.Parameters.AddWithValue("@newStatus", 0);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }
            catch
            {

            }

        }

        public void UpdateEnabled(string username)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE UserAccounts SET active_status = @newStatus WHERE username = @username", conn);
                cmd.Parameters.AddWithValue("@newStatus", 1);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }
            catch
            {

            }

        }



    }

    public class AppSettings
    {
        public string Secret { get; set; }
    }

    public class DataObj
    {
        public List<string> creditentials { get; set; }
    }
    public class DataObj2
    {
        public List<string> authen { get; set; }
    }

}
