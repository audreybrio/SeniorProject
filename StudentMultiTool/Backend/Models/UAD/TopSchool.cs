namespace StudentMultiTool.Backend.Models.UAD
{
    public class TopSchool
    {

        public string schoolName { get; set; }

        public int schoolCount { get; set; }

        public TopSchool(string schoolName, int schoolCount)
        {
            this.schoolName = schoolName;
            this.schoolCount = schoolCount;
        }

    }
}
