namespace StudentMultiTool.Backend.Models.Matching
{
    public class Match
    {
        // Initializing 
        public string match { get; set; }

        public int matchId { get; set; }

        public string reason { get; set; }  

        public string?  overlap { get; set; } 

        // Saving match information with username 
        public Match(string match, string reason, string overlap)
        {
            this.match = match;
            this.reason = reason;
            this.overlap = overlap;
        }

        
        // Saving match information with an id
        public Match(int matchId, string reason, string overlap)
        {
            this.matchId = matchId;
            this.reason = reason;
            this.overlap=overlap;
        }

        // Gets the name of a user 
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
