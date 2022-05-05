namespace StudentMultiTool.Backend.Services.GPA_Calc
{
    public class Grade
    {

        public bool CalculateGrade(string username, string course, List<string> grades, List<string> outOf)
        {
            float total = 0;
            for (int i = 0; i < grades.Count; i++)
            {
                total = total + Int32.Parse(grades[i]) / Int32.Parse(outOf[i]);
            }
            return true;
        }

        public List<string> DisplayRanking(string course)
        {
            var grades = new List<string>();
            return grades;
        }
    }
}
