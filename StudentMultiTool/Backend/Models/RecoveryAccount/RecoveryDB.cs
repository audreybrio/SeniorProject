using System.Data.SqlClient;
using System.Net.Mail;


namespace StudentMultiTool.Backend.Models.RecoveryAccount
{
    public class RecoveryDB
    {

        public bool sendEmailPasswordReset(RecoveryUserEmail recovery)
        {
            //string connection = @"Server=(localdb)\MSSQLLocalDB;Database=Marvel;Trusted_Connection=True; MultipleActiveResultSets=true;";

            bool result = true;
            MailMessage mail = new MailMessage();
            string baseURL = "https://localhost:5003";
            mail.From = new MailAddress("studentmultitool@outlook.com");
            mail.To.Add(new MailAddress(recovery.email));
            mail.Subject = "Email link to reset existing account with Student Multi-Tool";
            string body = "Thank you for being our users!\n" +
                          "This is the link will give you access to reset your password.\n \n" +
                          "------------------------------\n" +
                          "Username: " + recovery.username + "\n" +
                          "------------------------------\n\n" +
                          "Please click the link to reset your account password: " +
                          baseURL + "/newpassword"; /*+ username + "/" + token;*/
            mail.Body = body;
            mail.Priority = MailPriority.Normal;
            using (SmtpClient client = new SmtpClient("email-smtp.us-west-1.amazonaws.com", 587))
            {
                client.EnableSsl = true;
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
        /*string baseURL = "https://localhost:5002";
            mail.From = new MailAddress("studentmultitool@outlook.com");
            mail.To.Add(new MailAddress(email));
            mail.Subject = "Email verification for Student Multi-Tool new account";
            string body = "Thanks for signing up!\n" +
                          "Your account has been created. You can login with the following credentials after you have activated your account by clicking the URL below.\n \n" +
                          "------------------------------\n" +
                          "Username: " + username + "\n" +
                          "Email: " + email + "\n" +
                          "------------------------------\n\n" +
                          "Please click the link to activate your account: " +
                          baseURL + "/RegistrationForm/" + username + "/" + token;
            mail.Body = body;
            mail.Priority = MailPriority.Normal;*/

        public bool sendNewPasswordReset (RecoveryPassoward rp)
        {
            bool result;

            try
            {
                string connection = @"Server=(localdb)\MSSQLLocalDB;Database=Marvel;Trusted_Connection=True; MultipleActiveResultSets=true;";


                using (SqlConnection con = new SqlConnection(connection))
                {
                    String query = "INSERT INTO [Recovery] (password, confirmPassword) VALUES (@p, @cp);";

                    SqlCommand cmd = new SqlCommand(query, con);

                    try
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@p", rp.password);
                        cmd.Parameters.AddWithValue("@cp", rp.confirmpassword);
                        cmd.ExecuteNonQuery();
                        result = true;
                        con.Close();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        result = false;
                    }
                }
            }
            catch (Exception e)
            {
                result = false;
                Console.WriteLine(e);
            }

            return result;
        }

    }
}
