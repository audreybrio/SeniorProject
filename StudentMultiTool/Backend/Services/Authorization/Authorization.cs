using System;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Services.Authorization
{
    public class auth
    {
        
        public static string getUserRole(string username )
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT role" + " from UserTable " + "WHERE UserTable.role = role", conn);
            cmd.ExecuteNonQuery();
            string role = "";
            role = (string)cmd.ExecuteScalar();

            return role;
        }


       
    }
}
