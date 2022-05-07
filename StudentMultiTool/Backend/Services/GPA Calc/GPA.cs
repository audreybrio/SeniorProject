namespace StudentMultiTool.Backend.Services.GPA_Calc
{
    public class GPA
    {
        // Calculates given gpa for a user 
        public double CalculateGPA(List<double> grades, List<int> units)
        {
            int totalUnits = 0;
            double totalPoints = 0;
            double gpa = 0;
            // Loops to get total points
            for (int i = 0; i < grades.Count; i++)
            {
                totalPoints = totalPoints + grades[i] * units[i];
            }
            totalUnits = units.Sum();
            // Calculates gpa
            gpa = totalPoints / totalUnits;
            // Rounds off
            double roundedGpa = Math.Round(gpa,3);
            return roundedGpa;
        }
    }
}
