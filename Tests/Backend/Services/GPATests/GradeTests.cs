using System;
using System.Collections.Generic;
using StudentMultiTool.Backend.Services.GPA_Calc;
using StudentMultiTool.Backend.DAL;
using StudentMultiTool.Backend.Models.GPACalc;

using Xunit;

namespace Tests.Backend.Services.GPATests
{
    public class GradeTests
    {
        // Cacluates correct grade
        [Fact]
        public void CalculateCorrectGrade()
        {
            List<double> assignment = new List<double>();
            List<int> points = new List<int>();
            assignment.Add(4);
            assignment.Add(4.5);
            assignment.Add(8);
            assignment.Add(9);
            points.Add(10);
            points.Add(15);
            points.Add(13);
            points.Add(10);
            Grade calcGrade = new Grade();
            double grade = calcGrade.CalculateGrade(assignment, points);
            Assert.Equal(53.125, grade);

            List<double> assignment2 = new List<double>();
            List<int> points2 = new List<int>();
            assignment2.Add(60);
            assignment2.Add(84);
            assignment2.Add(80);
            assignment2.Add(72);
            points2.Add(100);
            points2.Add(150);
            points2.Add(100);
            points2.Add(100);
            Grade calcGrade2 = new Grade();
            double grade2 = calcGrade2.CalculateGrade(assignment2, points2);
            Assert.Equal(65.778, grade2);

        }

        // Calculates incorrect grade
        [Fact]
        public void CalculateIncorrectGrade()
        {
            List<double> assignment = new List<double>();
            List<int> points = new List<int>();
            assignment.Add(4);
            assignment.Add(4.5);
            assignment.Add(8);
            assignment.Add(9);
            points.Add(10);
            points.Add(15);
            points.Add(13);
            points.Add(11);
            Grade calcGrade = new Grade();
            double grade = calcGrade.CalculateGrade(assignment, points);
            Assert.NotEqual(53.125, grade);

            List<double> assignment2 = new List<double>();
            List<int> points2 = new List<int>();
            assignment2.Add(60);
            assignment2.Add(84);
            assignment2.Add(80);
            assignment2.Add(72);
            points2.Add(100);
            points2.Add(150);
            points2.Add(100);
            points2.Add(101);
            Grade calcGrade2 = new Grade();
            double grade2 = calcGrade2.CalculateGrade(assignment2, points2);
            Assert.NotEqual(65.778, grade2);

        }

        // Saves grade correctly
        [Fact]
        public void SaveGradeSuccess()
        {
            string course = "CECS 491B";
            int section = 5;
            List<double> assignment = new List<double>();
            List<int> points = new List<int>();
            assignment.Add(4);
            assignment.Add(4.5);
            assignment.Add(8);
            assignment.Add(9);
            points.Add(10);
            points.Add(15);
            points.Add(13);
            points.Add(10);
            Grade calcGrade = new Grade();
            double grade = calcGrade.CalculateGrade(assignment, points);
            Assert.Equal(53.125, grade);
            bool isUpdated = calcGrade.SaveGrade("abrio", course, grade, section);
            Assert.True(isUpdated);
            bool isInserted = calcGrade.SaveGrade("atoscano", course, grade, section);
            Assert.True(isInserted);


        }

        // Doesnt save grade correctly 
        [Fact]
        public void SaveGradeFail()
        {
            string course = "CECS 491B";
            int section = 5;
            List<double> assignment = new List<double>();
            List<int> points = new List<int>();
            assignment.Add(4);
            assignment.Add(4.5);
            assignment.Add(8);
            assignment.Add(9);
            points.Add(10);
            points.Add(15);
            points.Add(13);
            points.Add(101);
            Grade calcGrade = new Grade();
            double grade = calcGrade.CalculateGrade(assignment, points);
            Assert.NotEqual(53.125, grade);
            GradeDAL gradeDal = new GradeDAL();
            bool isInserted = gradeDal.SaveGradeInsert("abrio", course, grade, section);
            Assert.False(isInserted);
        }

        // Displays rankings lists not equal 
        [Fact]
        public void DisplayRanking()
        {
            List<GradeModel> ranking = new List<GradeModel>();
            List<GradeModel> rankingTesting = new List<GradeModel>();
            string course = "CECS 491B";
            int section = 5;
            ranking.Add(new GradeModel(1, course, section, 67.89));
            ranking.Add(new GradeModel(2, course, section, 55.34));
            ranking.Add(new GradeModel(3, course, section, 53.125));
            ranking.Add(new GradeModel(4, course, section, 53.125));

            Grade displayRank = new Grade();
            rankingTesting = displayRank.DisplayRanking(course, section);
            Assert.NotEqual(ranking, rankingTesting);

        }


    }
}
