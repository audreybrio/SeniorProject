using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StudentMultiTool.Backend.Services.Email
{
    internal class SendEmail
    {
        const string smtpClient = "email-smtp.us-east-1.amazonaws.com";
        const string sendFrom = "studentmultitool@outlook.com";
        const string id = "AKIA4LFTDFRCSQHGW2BL";
        const string password = "BMAUAXuLN+qSGL0QiezLwtqpfckzibBAwvJ/0AiDtrQa";

        public bool SendOTPEmail(string email, string otp)
        {
            try
            {
                String from = sendFrom;
                String subject = "OTP code for Student Multi-Tool";
                String msg = otp;
                String to = email;
                MailMessage mail = new MailMessage(from, to, subject, msg);
                SmtpClient client = new SmtpClient(smtpClient);
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential(id, password);
                client.EnableSsl = true;
                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
