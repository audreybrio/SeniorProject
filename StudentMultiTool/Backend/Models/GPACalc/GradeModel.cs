namespace StudentMultiTool.Backend.Models.GPACalc
{
    public class GradeModel
    {
        public int id { get; set; }
        public string course { get; set; }

        public int section { get; set; }

        public double grade { get; set; }

        public GradeModel(int id, string course, int section, double grade)
        {
            this.id = id;
            this.course = course;
            this.section = section;
            this.grade = grade;
        }


    }

}
