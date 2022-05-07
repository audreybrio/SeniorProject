using System.Data.SqlClient;
using StudentMultiTool.Backend.Models.GPACalc;

namespace StudentMultiTool.Backend.DAL
{
    public class GradeDAL
    {
        // Connection string 
        const string connectionString = "MARVELCONNECTIONSTRING";

        // SQL to insert grade if doesnt alreeady exist 
        public bool SaveGradeInsert(string username, string course, double grade, int section)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Grades (userId, course, section, grade) VALUES ((SELECT id FROM UserAccounts WHERE UserAccounts.username = @username), @course, @section, @grade)", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@section", section);
                cmd.Parameters.AddWithValue("@grade", grade);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // SQL to update grade if already exists 
        public bool SaveGradeUpdate(string username, string course, double grade, int section)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Grades SET grade = @grade WHERE userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND course = @course AND section = @section", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@section", section);
                cmd.Parameters.AddWithValue("@grade", grade);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // SQL to check if grade is already in the database
        public bool GradeExists(string username, string course, int section)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT (userId) FROM Grades WHERE userId = (SELECT id FROM UserAccounts WHERE UserAccounts.username = @username) AND course = @course AND section = @section", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@section", section);
                int countGrades = 0;
                countGrades = (int)cmd.ExecuteScalar();
                conn.Close();
                if (countGrades != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }


        }

        // SQL to get lists of grades to display 
        public List<GradeModel> DisplayRanking(string course, int section)
        {
            List<GradeModel> rankings = new List<GradeModel>();
            int id = 1;
            try
            {

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT grade FROM Grades WHERE course = @course AND section = @section order by grade desc", conn);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@section", section);
                double grade = 0.0;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    grade = dr.GetDouble(0);

                    GradeModel gradeModel = new GradeModel(id, course, section, grade);
                    rankings.Add(gradeModel);
                    id++;

                }
                dr.Close();
                conn.Close();
                return rankings;

        }
            catch(Exception ex)
            {
                return rankings;
            }
}
    }
}
