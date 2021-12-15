using System;
using System.Data.SqlClient;

namespace UserManagement
{
    public class UserManager
    {
        public static bool DeleteUser(string username)
        {
            bool userExist = Validate.UserExist(username);
            if (userExist == true)
            {
                int temp;
                temp = Update.UpdateDelete(username);
                bool t = Evaluate.Eval(temp);
                if (t == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public static bool EnableUser(string username)
        {
            bool userExist = Validate.UserExist(username);
            if(userExist == true)
            {
                bool isDisabled = Validate.CheckDisabled(username);
                if (isDisabled == true)
                {
                    Update.UpdateEnable(username);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public static bool DisableUser(string username)
        {
            bool userExist = Validate.UserExist(username);
            if (userExist == true)
            {
                bool isEnabled = Validate.CheckEnabled(username);
                if (isEnabled == true)
                {
                    Update.UpdateDisable(username);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public static bool UpdateRoleUser(string username, string role)
        {
            bool userExist = Validate.UserExist(username);
            if (userExist == true)
            {
                bool isUpdated = Validate.UserRoleCheck(username, role);
                if (isUpdated == true)
                {
                    Update.UpdateRole(username, role);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }


    }

    public class Authorize
    {
        static bool authorize(string username)
        {
            //authorize
            return true;
        }
    }
    public class Validate
    {
        public static bool UserExist(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd3 = new SqlCommand("SELECT COUNT (username)" + " from UserTable " + "WHERE UserTable.username = @username", conn);
            cmd3.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd3.ExecuteReader();
            int count = 1;
            reader.Close();
            count = (int)cmd3.ExecuteScalar();
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckDisabled(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT active_status" + " from UserTable" + " WHERE UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            bool active_status = (bool)cmd.ExecuteScalar();
            if (active_status == false)
            { 
                return true;
            }
            else 
            {
                return false;

            }

        }


        public static bool CheckEnabled(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT active_status" + " from UserTable" + " WHERE UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            bool active_status = (bool)cmd.ExecuteScalar();
            if (active_status == true)
            {
                return true;
            }
            else
            {
                return false;
            }
          
        }


        public static bool UserRoleCheck(string username, string role)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT (username)" + " from UserTable" + " WHERE UserTable.username = @username AND UserTable.role = @role", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@role", role);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            int count = (int)cmd.ExecuteScalar();
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




    }

    public class Update
    {
        public static int UpdateDelete(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM UserTable WHERE UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT (username)" + " from UserTable " + "WHERE UserTable.username = @username", conn);
            cmd2.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd2.ExecuteReader();
            int count = 1;
            reader.Close();
            count = (int)cmd2.ExecuteScalar();
            return count;
        }

        public static void UpdateEnable(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserTable" + " SET active_status = @newStatus" + " WHERE username = @username", conn);
            cmd.Parameters.AddWithValue("@newStatus", 1);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
         }

        public static void UpdateDisable(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserTable" + " SET active_status = @newStatus" + " WHERE username = @username", conn);
            cmd.Parameters.AddWithValue("@newStatus", 0);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
        }

        public static void UpdateRole(string username, string newRole)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserTable" + " SET role = @role" + " WHERE username = @username", conn);
            cmd.Parameters.AddWithValue("@role", newRole);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
        }



    }

    public class Evaluate
    {
        public static bool Eval(int rowsAffected)
        {
            if (rowsAffected == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string Eval(bool affected)
        {
            if (affected == true)
            {
                string a = "successfully deleted.";
                return a;
            }
            else
            {
                string a = "Error: ";
                return a;
            }
        }


        static void Main(string[] args)
        {
           // string user = "jcutri";
            //string role = "student";
            //bool t = UserManager.UpdateRoleUser(user,role);
            
        }
    }
}
