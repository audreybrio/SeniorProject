namespace StudentMultiTool.Backend.Models.UAD
{
    public class AverageDuration
    {
        public double duration { get; set; }

        public string name { get; set; }

        public AverageDuration()
        {
            duration = 0;
            name = string.Empty;
        }

        public AverageDuration(double duration, string name)
        {
            this.duration = duration;
            this.name = name;
        }


    }
}
