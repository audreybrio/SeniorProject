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

        public bool sendNewPasswordReset (RecoveryPassoward rp, string email)
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
                    String query = "UPDATE [UserAccounts] SET UserAccounts.passcode = @passcode where UserAccounts.email = @email ;";

                    SqlCommand cmd = new SqlCommand(query, con);

                    try
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@email", email);
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

        /*
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
        }*/

    }
}
