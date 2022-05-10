namespace StudentMultiTool.Backend.Models.UAD
{
    public class MostVisited
    {

        public string viewName { get; set; }

        public int viewCount { get; set; }  

        public MostVisited(string viewName, int viewCount)
        {
            this.viewName = viewName;
            this.viewCount = viewCount; 
        }
    }
}
