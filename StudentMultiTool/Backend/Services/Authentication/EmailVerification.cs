using System.Net.Mail;

namespace StudentMultiTool.Backend.Services.Authentication
{
    public class EmailVerification
    {
        public EmailVerification() { }

        public void SendEmail(string email, string uniqueIDcode)
        {
            String msg = uniqueIDcode;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("studentmultitool@outlook.com");
            mail.To.Add(new MailAddress(email));
            mail.Subject = "Email verification for Student Multi-Tool";
            string body = "We are excited to tell you that your account is " +
                          "successfully created. Please click on the link below to verify your account.\n" +
                          "www.google.com";// we have to add our URL + uniqueIDcode
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
