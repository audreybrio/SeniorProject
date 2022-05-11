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

        public bool SendEmailVerification(string email, string token)
        {
            string baseURL = "https://localhost:5002";
            // string baseURL = "http://studentmultitool.me";
            try
            {
                String from = sendFrom;
                String subject = "Email verification for Student Multi-Tool new account";
                String msg = "Thanks for signing up!\n" +
                          "Your account has been created. You can login with your credentials after you have activated your account by clicking the URL below.\n \n" +
                          "Please click the link to activate your account: " +
                          baseURL + "/RegistrationForm/" + token;
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
