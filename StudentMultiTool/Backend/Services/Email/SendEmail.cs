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
        const string baseURL = "https://localhost:5002";
        // string baseURL = "http://studentmultitool.me";
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
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool SendEmailVerification(string email, string token)
        {
           
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
                Console.WriteLine(ex);
                return false;
            }
        }


        public bool sendEmailPasswordReset(string email)
        {

            try
            {
                String from = sendFrom;
                String subject = "Email link to reset existing account with Student Multi-Tool";
                String msg = "Thank you for being our users!\n" +
                          "This is the link will give you access to reset your password.\n \n" +
                          "Please click the link to reset your account password: " +
                          baseURL + "/newpassword";
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
                Console.WriteLine(ex);
                return false;
            }
          
        }


        public bool sendEmailDisabledAccount(string email)
        {
            try
            {
                String from = sendFrom;
                String subject = "Email link to reset existing account with Student Multi-Tool";
                String msg = "Thank you for being our users!\n" +
                          "This is the link will give you access to reactivate your account.\n \n" +
                          "Please click the link to reactivate your account password: " +
                          baseURL + "/activateaccount";
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
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
