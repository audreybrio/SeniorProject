using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using StudentMultiTool.Backend.DAL;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace StudentMultiTool.Backend.Services.Authentication
{
    public class Authenticate
    {

        public bool EmailPasscodeCheck(string email, string passcode)
        {
            bool valPasscode, valEmail, doesExist;
            Validate val = new Validate();
            valPasscode = val.ValidatePasscode(passcode);
            valEmail = val.ValidateEmail(email);

            if (valPasscode && valEmail)
            {
                LoginDAL loginDAL = new LoginDAL();
                string hashedPasscode = HashPass(passcode);
                doesExist = loginDAL.EmailPasscodeCheck(email, hashedPasscode);
                if (doesExist == true)
                {
                    string otp = Randomize();
                    loginDAL.Randomize(email, otp);
                    bool emailSent = SendEmail(email, otp);
                    return emailSent;
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

        public string AuthenticateUser(string username, string otp)
        {
            LoginDAL loginDAL = new LoginDAL();
            int count = loginDAL.LoginUser(username, otp);

            if (count > 0)
            {
                string role = loginDAL.GetRole(username);
                var token = GenerateJwtToken(username, role);
                return token;
            }
            else
            {
                LogIP log = new LogIP();
                log.LoggingIP(username);
                return null;

            }


        }
        public bool DisableUser(string email)
        {
            LoginDAL loginDAL = new LoginDAL();
            loginDAL.UpdateDisable(email);
            return true;
        }

        public bool CheckDisabled(string email)
        {
            LoginDAL loginDAL = new LoginDAL();
            bool isDisabled = loginDAL.CheckDisabled(email);
            Console.WriteLine(isDisabled);
            return isDisabled;

        }

        public int UserExists(string email)
        {
            LoginDAL loginDAL = new LoginDAL();
            int userExists = loginDAL.UserExists(email);
            return userExists;
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

        public string Randomize()
        {
            Random rand = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            string otp = new string(Enumerable.Repeat(chars, 8).Select(s => s[rand.Next(s.Length)]).ToArray());
            return otp;
        }

        public bool SendEmail(string email, string otp)
        {
            try
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string GenerateJwtToken(string username, string role)
        {
            try
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
            catch
            {
                return null;
            }


            
        }
    }
}
