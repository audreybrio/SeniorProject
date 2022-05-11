using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace StudentMultiTool.Backend.DAL
{
    public class LoginDAL
    {

        const string connectionString = "MARVELCONNECTIONSTRING";



        // Checks if user exists in database
        public bool EmailPasscodeCheck(string email, string passcode)
        {

            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT (username)" + " from UserAccounts " + "WHERE UserAccounts.email = @email AND UserAccounts.passcode = @passcode", conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@passcode", passcode);
                int count = 0;
                count = (int)cmd.ExecuteScalar();
                conn.Close();
                if (count > 0)
                {
                    // Checks is user is disabled
                    bool isDiasbled = CheckDisabled(email);
                    if (isDiasbled)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    //return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
 
        }


        // Checks is user is disabled
        public bool CheckDisabled(string email)
        {
            try
            {
               
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT active_status from UserAccounts WHERE UserAccounts.email = @email", conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
                bool active_status = (bool)cmd.ExecuteScalar();
                conn.Close();
                Console.WriteLine(active_status);
                return active_status;
            }
            catch(Exception ex)
            {
                Console.WriteLine("fucking failed");
                return false;
            }

        }

        public bool Randomize(string email, string otp)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();

                int countUser = UserOTPExists(email);
                int userId = GetUserId(email);
                DateTime timeStamp = DateTime.Now;

                if (countUser == 0)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO OTP (timestamp, otp, userID) VALUES (@timestamp, @otp, @userID)", conn);
                    cmd.Parameters.AddWithValue("@timestamp", timeStamp);
                    cmd.Parameters.AddWithValue("@otp", otp);
                    cmd.Parameters.AddWithValue("@userID", userId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("UPDATE OTP SET timestamp = @timestamp, otp = @otp WHERE userId = @userID", conn);
                    cmd.Parameters.AddWithValue("@timestamp", timeStamp);
                    cmd.Parameters.AddWithValue("@otp", otp);
                    cmd.Parameters.AddWithValue("@userID", userId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public int UserOTPExists(string email)
        {

            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand c = new SqlCommand("SELECT COUNT(id) FROM OTP WHERE OTP.userId = (SELECT id FROM UserAccounts WHERE UserAccounts.email = @email)", conn);
                c.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = c.ExecuteReader();
                int id = 0;
                reader.Close();
                id = (int)c.ExecuteScalar();
                conn.Close();
                return id;
            }
            catch
            {
                return 0;
            }

        }

        public int UserExists(string email)
        {

            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand c = new SqlCommand("SELECT COUNT(id) FROM UserAccounts WHERE UserAccounts.email = @email", conn);
                c.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = c.ExecuteReader();
                int id = 0;
                reader.Close();
                id = (int)c.ExecuteScalar();
                conn.Close();
                return id;
            }
            catch
            {
                return 0;
            }

        }

        public int GetUserId(string email)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand c = new SqlCommand("SELECT id FROM UserAccounts WHERE UserAccounts.email = @email", conn);
                c.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = c.ExecuteReader();
                int id = 0;
                reader.Close();
                id = (int)c.ExecuteScalar();
                conn.Close();
                return id;
            }
            catch
            {
                return 10;
            }

        }





        // Validates user entered correct credentials to enter system
        public int LoginUser(string username, string password)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(otp)" + " from OTP WHERE " + "OTP.userID = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND OTP.otp = @password", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = cmd.ExecuteReader();
                int count = 0;
                reader.Close();
                count = (int)cmd.ExecuteScalar();
                conn.Close();
                return count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        // Checks is OTP is still valid
        public static bool ValidTime(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
            conn.Open();
            SqlCommand c = new SqlCommand("SELECT id FROM UserAccounts WHERE UserAccounts.username = @username", conn);
            c.Parameters.AddWithValue("@username", username);
            SqlDataReader read = c.ExecuteReader();
            int id = 0;
            read.Close();
            if (c.ExecuteScalar() == null)
            {
                return false;
            }
            id = (int)c.ExecuteScalar();

            SqlCommand cds = new SqlCommand("SELECT timestamp FROM Otp WHERE Otp.userId = @userId", conn);
            cds.Parameters.AddWithValue("@userId", id);
            cds.ExecuteNonQuery();
            DateTime time = (DateTime)cds.ExecuteScalar();

            DateTime localTime = DateTime.Now;

            DateTime validTime = time.AddHours(24);
            conn.Close();
            int compare = (validTime.CompareTo(localTime));
            if (compare >= 0)
            {
                return true;
            }
            else { return false; }
        }


        public string GetRole(string username)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand c = new SqlCommand("SELECT role FROM UserAccounts WHERE UserAccounts.username = @username", conn);
                c.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = c.ExecuteReader();
                string role = "";
                reader.Close();
                role = (string)c.ExecuteScalar();
                conn.Close();
                return role;
            }
            catch
            {
                return "student";
            }
        }



        // Disables a user if they exceed 5 incorrect login attempts
        public void UpdateDisable(string email)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE UserAccounts SET active_status = @newStatus WHERE email = @email", conn);
                cmd.Parameters.AddWithValue("@newStatus", 0);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {

            }

        }

        public void UpdateEnable(string email)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE UserAccounts SET active_status = @newStatus WHERE email = @email", conn);
                cmd.Parameters.AddWithValue("@newStatus", 1);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {

            }

        }
    }
}
