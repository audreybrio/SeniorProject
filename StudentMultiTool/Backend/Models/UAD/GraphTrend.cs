namespace StudentMultiTool.Backend.Models.UAD
{
    public class GraphTrend
    {

        public List<int> days { get; set; }
        public List<int> count { get; set; }

        public GraphTrend()
        {
            days = new List<int>();
            count = new List<int>();
        }

        public GraphTrend(List<int> days, List<int> count)
        {
            this.days = days;
            this.count = count;
        }
    }
}
