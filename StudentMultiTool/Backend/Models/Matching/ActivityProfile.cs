namespace StudentMultiTool.Backend.Models.Matching
{
    public class ActivityProfile
    {

        public int userId { get; set; }

        public string activity1 { get; set; }
        public string? activity2 { get; set; }
        public string? activity3 { get; set; }
        public string? activity4 { get; set; }
        public string? activity5 { get; set; }

        public List<string> activities { get; set; }

    }
}
