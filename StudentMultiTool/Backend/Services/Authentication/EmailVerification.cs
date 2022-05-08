using System.Net.Mail;
using StudentMultiTool.Backend.Models.RecoveryAccount;

namespace StudentMultiTool.Backend.Services.Authentication
{
    public class EmailVerification
    {
        // URL Link 
        public string baseURL = "https://localhost:5002";

        public EmailVerification() { }
        
        public bool sendEmailPasswordReset(RecoveryUserEmail recovery)
        {
            bool result = false;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("studentmultitool@outlook.com");
            mail.To.Add(new MailAddress(recovery.email));
            mail.Subject = "Email link to reset existing account with Student Multi-Tool";
            string body = "Thank you for being our users!\n" +
                          "This is the link will give you access to reset your password.\n \n" +
                          "------------------------------\n" +
                          "Username: " + recovery.username + "\n" +
                          "------------------------------\n\n" +
                          "Please click the link to reset your account password: " +
                          baseURL + "/newpassword"; 
            mail.Body = body;
            mail.Priority = MailPriority.Normal;
            using (SmtpClient client = new SmtpClient("email-smtp.us-east-1.amazonaws.com", 587))
            {

                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("AKIA4LFTDFRCSQHGW2BL", "BMAUAXuLN+qSGL0QiezLwtqpfckzibBAwvJ/0AiDtrQa");

                try
                {
                    client.Send(mail);
                    result = true;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Exception caught in SendEmail(): {0}", ex.ToString());
                    result = false;
                }
            }

            return result;
        }


        public bool sendEmailDisabledAccount(RecoveryUserEmail disable)
        {

            bool result = false;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("studentmultitool@outlook.com");
            mail.To.Add(new MailAddress(disable.email));
            mail.Subject = "Email link to reset existing account with Student Multi-Tool";
            string body = "Thank you for being our users!\n" +
                          "This is the link will give you access to reactivate your account.\n \n" +
                          "------------------------------\n" +
                          "Username: " + disable.username + "\n" +
                          "------------------------------\n\n" +
                          "Please click the link to reactivate your account password: " +
                          baseURL + "/activateaccount";
            mail.Body = body;
            mail.Priority = MailPriority.Normal;
            using (SmtpClient client = new SmtpClient("email-smtp.us-east-1.amazonaws.com", 587))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("AKIA4LFTDFRCSQHGW2BL", "BMAUAXuLN+qSGL0QiezLwtqpfckzibBAwvJ/0AiDtrQa");
                try
                {
                    client.Send(mail);
                    result = true;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Exception caught in SendEmail(): {0}", ex.ToString());
                    result = false;
                }
            }

            return result;
        }

        // SendEmail method to verify email and activate the new user account
        public void SendEmail(string email, string token)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("studentmultitool@outlook.com");
            mail.To.Add(new MailAddress(email));
            mail.Subject = "Email verification for Student Multi-Tool new account";
            string body = "Thanks for signing up!\n" +
                          "Your account has been created. You can login with your credentials after you have activated your account by clicking the URL below.\n \n" +
                          "Please click the link to activate your account: " + 
                          baseURL + "/RegistrationForm/" + token;
            mail.Body = body;
            mail.Priority = MailPriority.Normal;
            using (SmtpClient client = new SmtpClient("email-smtp.us-west-1.amazonaws.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("AKIAW7HGHSCLGUZQVTOS", "BJxJD2wRP3HgnF6B0WKhEoNGgcmeOOZcSA5ZhOT7oQeR"); //change username and password to your email account username and password
                
                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Exception caught in SendEmail(): {0}", ex.ToString());
                }
            }
        }
        
    }
}
