using System.Text.Json.Nodes;

namespace StudentMultiTool.Backend.Models.ScheduleBuilder
{
    // Represents a ScheduleItem and any additional data that may be needed
    // to perform CRUD operations. Intended for use within the ScheduleController class.
    public class ScheduleItemCRUDModel
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public string Creator { get; set; }
        public string Contact { get; set; } = string.Empty;
        public List<bool> DaysOfWeek { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string Title { get; set; }

        public ScheduleItemCRUDModel(
            int Id,
            int ScheduleId,
            string Creator,
            string? Contact,
            List<bool> DaysOfWeek,
            int StartHour,
            int StartMinute,
            int EndHour,
            int EndMinute,
            string? Location,
            string? Notes,
            string Title
            )
        {
            this.Id = Id;
            this.ScheduleId = ScheduleId;
            this.Creator = Creator;
            if (Contact != null)
            {
                this.Contact = Contact;
            }
            this.DaysOfWeek = DaysOfWeek;
            this.StartHour = StartHour;
            this.StartMinute = StartMinute;
            this.EndHour = EndHour;
            this.EndMinute = EndMinute;
            if (Location != null)
            {
                this.Location = Location;
            }
            if (Notes != null)
            {
                this.Notes = Notes;
            }
            this.Title = Title;
        }
    }
}
