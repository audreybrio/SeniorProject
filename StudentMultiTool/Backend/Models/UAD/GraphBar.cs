namespace StudentMultiTool.Backend.Models.UAD
{
    public class GraphBar
    {

        public List<string> name { get; set; }
        public List<double> count { get; set; }

        public GraphBar()
        {
            name = new List<string>();
            count = new List<double>();
        }

        public GraphBar(List<string> name, List<double> count)
        {
            this.name = name;
            this.count = count;
        }

    }
}
