using System.Data.SqlClient;
using StudentMultiTool.Backend.Models.UAD;

namespace StudentMultiTool.Backend.DAL
{
    public class UadDAL
    {
        const string connectionString = "MARVELCONNECTIONSTRING";

        // SQL to get most visited page
        public List<MostVisited> MostVisited()
        {
            List<MostVisited> topVisited = new List<MostVisited>();
            try
            {

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT viewName, COUNT(viewName) FROM TopVisited group by viewName order by COUNT(viewName) desc", conn);
                int totalCount = 0;
                string viewName = "";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    viewName = dr.GetString(0);
                    totalCount = dr.GetInt32(1);

                    MostVisited newTopVisited = new MostVisited(viewName, totalCount);
                    topVisited.Add(newTopVisited);

                }
                dr.Close();
                conn.Close();
                return topVisited;
            }
            catch (Exception ex)
            {
                return topVisited;
            }
}

        // SQL to get school logins
        public List<TopSchool> TopSchool()
        {
            List<TopSchool> topSchool = new List<TopSchool>();
            try
            {

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT school, COUNT(school) FROM UserAccounts group by school order by COUNT(school) desc", conn);
                int totalCount = 0;
                string school = "";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    school = dr.GetString(0);
                    totalCount = dr.GetInt32(1);

                    TopSchool newTopVisited = new TopSchool(school, totalCount);
                    topSchool.Add(newTopVisited);

                }
                dr.Close();
                conn.Close();
                return topSchool;
            }
            catch (Exception ex)
            {
                return topSchool;
            }
        }
    }
}