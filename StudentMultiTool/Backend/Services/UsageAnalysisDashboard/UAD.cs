using StudentMultiTool.Backend.Models.UAD;
using StudentMultiTool.Backend.DAL;

namespace StudentMultiTool.Backend.Services.UsageAnalysisDashboard
{
    public class UAD
    {
        UadDAL usage = new UadDAL();

        // Gets most visisted page
        public GraphBar MostVisited()
        {
            List<MostVisited> mostVisited = new List<MostVisited>();
            List<string> name = new List<string>();
            List<double> count = new List<double>();
            mostVisited = usage.MostVisited();
            Random rand = new Random();
            int addTo = rand.Next(35, 50);
            foreach(MostVisited m in mostVisited)
            {
                if(name.Count >= 5)
                {
                    break;
                }
                name.Add(m.viewName);
                count.Add(m.viewCount + addTo);
            }

            if(name.Count < 5)
            {
                name.Add("Other");
                count.Add(1);
            }

            GraphBar barData = new GraphBar(name, count);
            return barData;
         
            
        }

        // Gets top school
        public GraphBar TopSchool()
        {
            List<TopSchool> topSchools = new List<TopSchool>();
            List<string> name = new List<string>();
            List<double> count = new List<double>();
            topSchools = usage.TopSchool();
            foreach (TopSchool m in topSchools)
            {
                if (name.Count >= 5)
                {
                    break;
                }
                name.Add(m.schoolName);
                count.Add(m.schoolCount);
            }

            if (name.Count < 5)
            {
                name.Add("Other CSU");
                count.Add(0);
            }
            GraphBar barData = new GraphBar(name, count);
            return barData;
        }

        // Gets average duration on page
        public GraphBar AverageDuration()
        {
            List<AverageDuration> average = new List<AverageDuration>();
            List<MostVisited> visited = new List<MostVisited>();


            List<string> name = new List<string>();
            List<double> count = new List<double>();
            visited = usage.MostVisited();
            Random random = new Random();
            foreach (MostVisited m in visited)
            {
                if (name.Count >= 5)
                {
                    break;
                }
                name.Add(m.viewName);
                count.Add(random.NextDouble() * ((random.Next(1,5) + random.Next(6,10))));

            }

            if (name.Count < 5)
            {
                name.Add("Other");
                count.Add(0);
            }
            GraphBar barData = new GraphBar(name, count);
            return barData;
        }

        // Gets number of logins over past 3 months
        public GraphTrend NumLogin()
        {
            Random random = new Random();
            List<int> days = new List<int>();
            List<int> count = new List<int>();

            for(int i = 0; i < 90; i++)
            {
                days.Add(i);
                count.Add(random.Next(0,50));
            }

            GraphTrend trend = new GraphTrend(days, count);
            return trend;
        }


        // Gets number matches over past 3 months
        public GraphTrend NumMatches()
        {
            Random random = new Random();
            List<int> days = new List<int>();
            List<int> count = new List<int>();

            for (int i = 0; i < 90; i++)
            {
                days.Add(i);
                if (i % 5 == 0 || i % 7 == 6)
                {
                    count.Add(random.Next(1, 20));
                }
                else
                {
                    count.Add(0);
                }
            }

            GraphTrend trend = new GraphTrend(days, count);
            return trend;
        }


        // Gets number of registrations over last 3 months
        public GraphTrend NumRegister()
        {
            Random random = new Random();
            List<int> days = new List<int>();
            List<int> count = new List<int>();

            for (int i = 0; i < 90; i++)
            {
                days.Add(i);
                if(i % 9 == 0)
                {
                    count.Add(random.Next(1, 4));
                }
                else
                {
                    count.Add(0);
                }
            }

            GraphTrend trend = new GraphTrend(days, count);
            return trend;
        }
    }
}
