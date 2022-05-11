using System.Net.Mail;

namespace StudentMultiTool.Backend.Services.Authentication
{
    public class EmailVerification
    {
        public EmailVerification() { }

        // SendEmail method to verify email and activate the new user account
        public void SendEmail(string email, string token)
        {
            MailMessage mail = new MailMessage();
            //string baseURL = "https://localhost:5002";
            string baseURL = "http://studentmultitool.me";
            mail.From = new MailAddress("studentmultitool@outlook.com");
            mail.To.Add(new MailAddress(email));
            mail.Subject = "Email verification for Student Multi-Tool new account";
            string body = "Thanks for signing up!\n" +
                          "Your account has been created. You can login with your credentials after you have activated your account by clicking the URL below.\n \n" +
                          "Please click the link to activate your account: " + 
                          baseURL + "/RegistrationForm/" + token;
            mail.Body = body;
            mail.Priority = MailPriority.Normal;
            //using (SmtpClient client = new SmtpClient("email-smtp.us-west-1.amazonaws.com", 587))
            using (SmtpClient client = new SmtpClient("email-smtp.us-east-1.amazonaws.com", 587))
            {
                client.EnableSsl = true;
                //client.Credentials = new System.Net.NetworkCredential("AKIAW7HGHSCLGUZQVTOS", "BJxJD2wRP3HgnF6B0WKhEoNGgcmeOOZcSA5ZhOT7oQeR"); //change username and password to your email account username and password
                client.Credentials = new System.Net.NetworkCredential("AKIA4LFTDFRCSQHGW2BL", "BMAUAXuLN+qSGL0QiezLwtqpfckzibBAwvJ/0AiDtrQa"); //change username and password to your email account username and password

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
