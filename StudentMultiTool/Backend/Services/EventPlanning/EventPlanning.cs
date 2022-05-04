namespace StudentMultiTool.Backend.Services.EventPlanning
{

    public class EventPlanning
    {
        private int _id;
        private string? _eventtitle, _eventtime, _date, _location, _description;

        public EventPlanning(int Id, string eventTitle, string eventTime, string Date, string Location, string Description)
        {
            this.Id = Id;
            this.eventTitle = eventTitle;
            this.eventTime = eventTime;
            this.Date = Date;
            this.Location = Location;
            this.Description = Description;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public String? eventTitle
        {
            get { return _eventtitle; }
            set { _eventtitle = value; }
        }
        public string? eventTime
        {
            get { return _eventtime; }
            set { _eventtime = value; }
        }
        public string? Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public string? Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }

    }


}
