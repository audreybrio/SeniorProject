using StudentMultiTool.Backend.Models.UAD;

namespace StudentMultiTool.Backend.Services.UsageAnalysisDashboard
{
    public class UAD
    {

        public bool MostVisited()
        {
            return true;
        }

        public bool GetGraphs()
        {
            return false;
        }

        public bool TopSchool()
        {
            TopSchool topSchool = new TopSchool("CSULB", 7);
            return true ;
        }
    }
}
