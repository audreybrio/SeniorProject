using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentMultiTool.Backend.Services.GPA_Calc;
using Xunit;


namespace Tests.Backend.Services.GPATests
{
    public class GPATesting
    {

        // Calculates correct gpa 
        [Fact]
        public void CalculateCorrectGpa()
        {
            List<double> grades = new List<double>();
            List<int> units = new List<int>();
            grades.Add(4.0);
            grades.Add(3.3);
            grades.Add(2.3);
            grades.Add(3.0);
            units.Add(3);
            units.Add(3);
            units.Add(3);
            units.Add(3);
            GPA calcGpa = new GPA();
            double gpa = calcGpa.CalculateGPA(grades, units);
            Assert.Equal(3.15, gpa);

        }

        // Calculates incorrect gpa 
        [Fact]
        public void CalculateIncorrectGpa()
        {
            List<double> grades = new List<double>();
            List<int> units = new List<int>();
            grades.Add(4.0);
            grades.Add(3.3);
            grades.Add(2.3);
            grades.Add(3.0);
            units.Add(3);
            units.Add(3);
            units.Add(3);
            units.Add(3);
            GPA calcGpa = new GPA();
            double gpa = calcGpa.CalculateGPA(grades, units);
            Assert.NotEqual(3.65, gpa);

        }
    }
}
