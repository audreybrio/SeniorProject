namespace StudentMultiTool.Backend.Services.GPA_Calc
{
    public class GPA
    {
        public double CalculateGPA(List<string> grades, List<string> units)
        {
            int totalUnits = 0;
            double totalPoints = 0;
            double gpa = 0;
            for (int i = 0; i < grades.Count; i++)
            {
                totalPoints = totalPoints + Int32.Parse(grades[i]) * Int32.Parse(units[i]);
            }

            for(int i = 0; i < units.Count; i++)
            {
                totalUnits = totalUnits + Int32.Parse(units[i]);
            }

            gpa = totalPoints / totalUnits;

            return gpa;
        }
    }
}
