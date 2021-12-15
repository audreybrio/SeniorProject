using System;
using System.Data.SqlClient;

namespace Disable{
    public class DisableUser{

        public static string getUserRole(string username)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT role" + " from UserTable " + "WHERE UserTable.role = role", conn);
            cmd.ExecuteNonQuery();
            string role = "";
            role = Convert.ToString(cmd.ExecuteScalar());
            return role;
        }

        public static bool userExist(string username){
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(username)" + " from UserTable" + " WHERE UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@username", userName);
            cmd.ExecuteNonQuery();
            int count = (int)cmd.ExecuteScalar();
            if(count > 0){
                return true;
            }
            else{
                return false;
            }
        }

        public static bool isEnable(string username){
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT active_status" + " from UserTable" + " WHERE UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@username", userName);
            cmd.ExecuteNonQuery();
            int active_status = -1;
            active_status = (int)cmd.ExecuteScalar();
            if(active_status == 1){
                return true;
            }
            else if(active_status == 0){
                return false;
            }
            else{
                Console.WriteLine("Error: active status not found")
                return false;
            }
        }

        public static bool userDisable(string username){
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserTable" + " SET active_status = @newStatus" + " WHERE username = @username", conn);
            cmd.Parameters.AddWithValue("@newStatus", 0);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            return true;
        }

        // Main 
        static void Main(string[] args){
            string CurrentUsername = "abrio";
            if(getUserRole(CurrentUsername) == "admin"){
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
                conn.Open();
                Console.WriteLine("Enter username to disable account: ");
                string userSelected = Console.ReadLine();
                if(userExist(userSelected) == true){
                    if(isEnable(userSelected)){
                        userDisable(userSelected);
                    }
                }
                else{
                    Console.WriteLine("User status cannot be changed to DISABLE");
                }
            }
        }
    }
}