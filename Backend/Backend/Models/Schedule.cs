namespace StudentMultiTool.Backend.Models
{
    // Represents a user's schedule.
    public class Schedule
    {
        // The actual ScheduleItems for this Schedule.
        public List<ScheduleItem> Items { get; set; } = new List<ScheduleItem>();
        // Options for the ScheduleItems
        public int? Id { get; set; } = null;
        public string Title { get; set; }
        // These are just user IDs
        public List<int> Collaborators { get; set; } = new List<int>();
        public int OwnerId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Schedule(int OwnerId, DateTime Created, DateTime Modified, string Title)
        {
            this.OwnerId = OwnerId;
            this.Created = Created;
            this.Modified = Modified;
            this.Title = Title;
        }
        public Schedule(int Id, int OwnerId, DateTime Created, DateTime Modified, string Title)
        {
            this.Id = Id;
            this.OwnerId = OwnerId;
            this.Created = Created;
            this.Modified = Modified;
            this.Title = Title;
        }
        public void AddScheduleItem(ScheduleItem newItem)
        {
            try
            {
                Items.Add(newItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
