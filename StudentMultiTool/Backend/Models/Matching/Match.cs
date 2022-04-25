namespace StudentMultiTool.Backend.Models.Matching
{
    public class Match
    {

        public string match { get; set; }

        public int matchId { get; set; }

        public string reason { get; set; }  

        public DateTime?  overlap { get; set; } 


        public Match(string match, string reason, DateTime overlap)
        {
            this.match = match;
            this.reason = reason;
            this.overlap = overlap;
        }

        public Match(string match,string reason)
        {
            this.match=match;
            this.reason=reason;
        }

        public Match(int matchId, string reason)
        {
            this.matchId = matchId;
            this.reason = reason;
        }

    }
}
