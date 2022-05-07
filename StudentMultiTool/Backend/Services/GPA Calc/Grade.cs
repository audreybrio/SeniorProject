using StudentMultiTool.Backend.DAL;
using StudentMultiTool.Backend.Models.GPACalc;
namespace StudentMultiTool.Backend.Services.GPA_Calc
{
    public class Grade
    {
        // Calculates the grade from assignment points and total points
        public double CalculateGrade(List<double> grades, List<int> outOf)
        {
            try
            {
                double total = 0;
                double earnedPointTotal = grades.Sum();
                double totalPoints = outOf.Sum();
                total = (earnedPointTotal / totalPoints) * 100;
                // Rounding 
                double roundedGrade = Math.Round(total, 3);
                return roundedGrade;
            }
            catch
            {
                return 0;
            }

        }

        // Saves grade to be able to compare against others
        public bool SaveGrade(string username, string course, double grade, int section)
        {
            try
            {
                GradeDAL gradeDal = new GradeDAL();
                bool doesExist = gradeDal.GradeExists(username, course, section);
                bool isSuccessful;
                if (doesExist)
                {
                    isSuccessful = gradeDal.SaveGradeUpdate(username, course, grade, section);
                }
                else{
                    isSuccessful = gradeDal.SaveGradeInsert(username, course, grade, section);
                }
                return isSuccessful;
            }
            catch
            {
                return false;
            }

        }

        // Gets list of grades to display 
        public List<GradeModel> DisplayRanking(string course, int section)
        {
            List<GradeModel> ranking = new List<GradeModel>();
            GradeDAL grade = new GradeDAL();
            ranking = grade.DisplayRanking(course, section);
            foreach (GradeModel rank in ranking)
            {
                Console.WriteLine(rank.id + "" + rank.course + " " + rank.grade);
            }
            return ranking;
        }
    }
}
