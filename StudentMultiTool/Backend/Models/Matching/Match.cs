namespace StudentMultiTool.Backend.Models.Matching
{
    public class Match
    {

        public string match { get; set; }

        public int matchId { get; set; }

        public string reason { get; set; }  

        public string?  overlap { get; set; } 


        public Match(string match, string reason, string overlap)
        {
            this.match = match;
            this.reason = reason;
            this.overlap = overlap;
        }

        

        public Match(int matchId, string reason, string overlap)
        {
            this.matchId = matchId;
            this.reason = reason;
            this.overlap=overlap;
        }


        public static string GetOverlap(string username, int id)
        {
            if (id ==2) { return "MW 4pm - 12am, TTH 3pm-7pm"; } 
            else if (id == 3) { return "MW 11am - 3pm, TTH 1pm-5pm; 7pm - 9pm, F 9am - 12pm"; }
            else if (id == 4) { return "MW 12pm - 2pm; 4pm - 9pm, TTH 2pm-6pm, F 10am - 4pm"; }
            else if (id == 5) { return "MW 4pm - 9pm, TTH 12pm-5pm; 7pm - 9pm, F 11am - 3pm"; }
            else if (id == 6) { return "MW 5:30pm - 8am, TTH 4pm-9pm"; }  
            else if (id == 7) { return "MW 4pm - 12am, TTH 3pm-7pm, F 3pm - 8pm" ; } 
            else{ return "No overlap"; }
        }

        public static string GetName(int id)
        {
            if (id == 2) { return "jdelgado"; }
            else if (id == 3) { return "bnickle"; }
            else if (id == 4) { return "atoscano"; }
            else if (id == 5) { return "jcutri"; }
            else if (id == 6) { return "stang"; }
            else if (id == 7) { return "dpatel"; }
            else { return "No overlap"; }
        }
    }
}
