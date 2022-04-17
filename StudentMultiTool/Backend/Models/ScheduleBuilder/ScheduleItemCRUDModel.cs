using System.Text.Json.Nodes;

namespace StudentMultiTool.Backend.Models.ScheduleBuilder
{
    // Represents a single, reoccurring daily or weekly event
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

        // Returns the ScheduleItem as a JsonObject.
        public JsonObject ToJson()
        {
            JsonObject result = new JsonObject
            {
                [ScheduleItemOptions.JsonTitle] = Title,
                [ScheduleItemOptions.JsonDays] = new JsonObject
                {
                    [ScheduleItemOptions.JsonSunday] = DaysOfWeek[0],
                    [ScheduleItemOptions.JsonMonday] = DaysOfWeek[1],
                    [ScheduleItemOptions.JsonTuesday] = DaysOfWeek[2],
                    [ScheduleItemOptions.JsonWednesday] = DaysOfWeek[3],
                    [ScheduleItemOptions.JsonThursday] = DaysOfWeek[4],
                    [ScheduleItemOptions.JsonFriday] = DaysOfWeek[5],
                    [ScheduleItemOptions.JsonSaturday] = DaysOfWeek[6]
                },
                [ScheduleItemOptions.JsonStart] = new JsonObject
                {
                    [ScheduleItemOptions.JsonHour] = StartHour,
                    [ScheduleItemOptions.JsonMinute] = StartMinute
                },
                [ScheduleItemOptions.JsonEnd] = new JsonObject
                {
                    [ScheduleItemOptions.JsonHour] = EndHour,
                    [ScheduleItemOptions.JsonMinute] = EndMinute
                },
                [ScheduleItemOptions.JsonLocation] = Location,
                [ScheduleItemOptions.JsonContact] = Contact,
                [ScheduleItemOptions.JsonNotes] = Notes,
                [ScheduleItemOptions.JsonCreator] = Creator
            };
            return result;
        }
    }
}
