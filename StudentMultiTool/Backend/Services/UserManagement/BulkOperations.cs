using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Services.UserManagement
{
    public class Program{
        public static string GetUserRole(string username)
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

        public static bool UserExist(string username){
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(username)" + " from UserTable" + " WHERE UserTable.username = @username", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            int count = (int)cmd.ExecuteScalar();
            if(count > 0){
                return true;
            }
            else{
                return false;
            }
        }

        public void demo()
        {
            string currentUsername = "abrio";
            if(GetUserRole(currentUsername) == "Admin"){
                string fileName = "bulkops.txt";
                string[] bulkOperLines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\" + fileName);
                for(int i = 0; i < bulkOperLines.Length; i++){
                    string[] singleOperation = bulkOperLines[i].Split(' '); // [0] = operation
                    if(singleOperation[0] == "Create User"){
                        if(!UserExist(singleOperation[1])){
                            Console.WriteLine("User Created");
                        }
                    }
                    else if(singleOperation[0] == "Delete User"){
                        if(UserExist(singleOperation[1])){
                            Console.WriteLine("User deleted");
                        }
                    }
                    else if(singleOperation[0] == "Update Role"){
                        if(UserExist(singleOperation[1])){
                            Console.WriteLine("User's role updated");
                        }
                    }
                    else if(singleOperation[0] == "Enable User"){
                        if(UserExist(singleOperation[1])){
                            Console.WriteLine("User Enabled");
                        }
                    }
                    else if(singleOperation[0] == "Disable User"){
                        if(UserExist(singleOperation[1])){
                            Console.WriteLine("User Disabled");
                        }
                    }
                }
            }
        }
    }
}