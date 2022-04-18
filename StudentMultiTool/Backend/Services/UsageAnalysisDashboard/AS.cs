using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMultiTool.Backend.Services.UsageAnalysisDashboard
{
    public class AS
    {
        public string View_name { get; set; }
        public int No_view { get; set; }
        public int Avg_duration { get; set; }
        public string Univ_name { get; set; }
        public int No_Ulogin { get; set; }
        public string Date { get; set; }
        public int No_login { get; set; }
        public int No_reg { get; set; }
        public int No_matched { get; set; }
    }
}