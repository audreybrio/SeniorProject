using System.Text.Json.Nodes;

namespace StudentMultiTool.Backend.Models.ScheduleBuilder
{
    // Represents a ScheduleItem and any additional data that may be needed
    // to perform CRUD operations. Intended for use within the ScheduleController class.
    public class ScheduleItemDTO
    {
        public int Id { get; set; } = -1;
        public int ScheduleId { get; set; } = -1;
        public string Creator { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public List<bool> DaysOfWeek { get; set; } = new List<bool>{ false, false, false, false, false, false, false };
        public int StartHour { get; set; } = 0;
        public int StartMinute { get; set; } = 0;
        public int EndHour { get; set; } = 0;
        public int EndMinute { get; set; } = 0;
        public string Location { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        public ScheduleItemDTO()
        {
        }

        public ScheduleItemDTO(
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
        public ScheduleItemDTO(ScheduleItem si)
        {
            this.Id = si.Id;
            if (!string.IsNullOrEmpty(si.Contact))
            {
                this.Contact = si.Contact;
            }
            this.DaysOfWeek = si.DaysOfWeek;
            this.StartHour = si.StartTime.Hour;
            this.StartMinute = si.StartTime.Minute;
            this.EndHour = si.EndTime.Hour;
            this.EndMinute = si.EndTime.Minute;
            if (!string.IsNullOrEmpty(si.Location))
            {
                this.Location = si.Location;
            }
            if (!string.IsNullOrEmpty(si.Notes))
            {
                this.Notes = si.Notes;
            }
            this.Title = si.Title;

            // TODO: these need correcting:
            this.ScheduleId = -1;
            this.Creator = si.Creator.ToString();
        }
    }
}
