using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using StudentMultiTool.Backend.Services.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;


namespace StudentMultiTool.Backend.Models.RecoveryAccount
{
    public class RecoveryDB
    {
        public string connection { get; set; }


        public RecoveryDB ()
        {
            connection = Environment.GetEnvironmentVariable(EnvironmentVariableEnum.CONNECTIONSTRING);

        }


        public bool sendEmailPasswordReset(RecoveryUserEmail recovery)
        {

            bool result = true;
            MailMessage mail = new MailMessage();
            string baseURL = "https://localhost:5002";
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

        public bool sendNewPasswordReset (RecoveryPassoward rp, int userID)
        {
            bool result;

            string s = "teammarvel";
            byte[] salt = Encoding.ASCII.GetBytes(s);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: rp.password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            try
            {


                using (SqlConnection con = new SqlConnection(connection))
                {
                    String query = "INSERT INTO [Recovery] (password, confirmPassword) VALUES (@p, @cp);";

                    SqlCommand cmd = new SqlCommand(query, con);

                    try
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@p", hashed);
                        cmd.Parameters.AddWithValue("@cp", hashed);
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

                 using (SqlConnection con = new SqlConnection(connection))
                 {
                    String query = "UPDATE [UserAccounts] SET UserAccounts.passcode = @passcode where UserAccounts.id = @userID ;";

                    SqlCommand cmd = new SqlCommand(query, con);

                    try
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@userID", userID);
                        cmd.Parameters.AddWithValue("@passcode", hashed);
                        Console.WriteLine("Update in UserAccounts");
                        cmd.ExecuteNonQuery();
                        result = true;
                        con.Close();
                    }
                    catch (Exception e)
                    {
                        result = false;
                        Console.WriteLine(e);
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


        public int getUserId(string email)
        {
            int ID = 1000;

            try
            {
                //string connection = @"Server=(localdb)\MSSQLLocalDB;Database=Marvel;Trusted_Connection=True; MultipleActiveResultSets=true;";

                using (SqlConnection con = new SqlConnection(connection))
                {
                    String query = "SELECT id" + " from UserAccounts WHERE UserAccounts.email = @email;";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    try
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        SqlDataReader rd = cmd.ExecuteReader();

                        while (rd.Read())
                        {
                            ID = Convert.ToInt32(rd["id"]);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ID;
        }

    }
}
