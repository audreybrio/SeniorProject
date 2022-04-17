using System.Text.Json.Nodes;

namespace StudentMultiTool.Backend.Models.ScheduleBuilder
{
    // Represents a single, reoccurring daily or weekly event
    public class ScheduleItem
    {
        public int Id { get; set; }
        
        // The user who created the ScheduleItem
        public int Creator { get; set; }

        // A contact for the ScheduleItem. This can represent a
        // college professor/instructor, a manager or supervisor,
        // or anyone else associated with the ScheduleItem.
        private string _Contact = string.Empty;
        public string Contact
        {
            get
            {
                return _Contact;
            }
            set
            {
                _Contact = shorten(value, ScheduleItemOptions.maxContactLength);
            }
        }

        // true represents the days of the week in which the ScheduleItem occurs.
        // false represents the days of the week in which the ScheduleItem doesn't occur.
        public List<bool> DaysOfWeek { get; set; } = new List<bool>
        {
            false,
            false,
            false,
            false,
            false,
            false,
            false
        };

        // The times at which the ScheduleItem begins and ends.
        public TimeOnly StartTime { get; set; } = new TimeOnly();
        public TimeOnly EndTime { get; set; } = new TimeOnly();

        // The physical location of the ScheduleItem.
        private string _Location = string.Empty;
        public string Location
        {
            get
            {
                return _Location;
            }
            set
            {
                _Location = shorten(value, ScheduleItemOptions.maxLocationLength);
            }
        }

        // Custom notes that the user can set for each ScheduleItem
        private string _Notes = string.Empty;
        public string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                _Notes = shorten(value, ScheduleItemOptions.maxNotesLength);
            }
        }

        // A user-assigned title/label for the ScheduleItem. Intended to
        // be a class name and/or number, such as "Calculus" or "MATH 123".
        private string _Title = string.Empty;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = shorten(value, ScheduleItemOptions.maxTitleLength);
            }
        }

        public ScheduleItem(int Id, int Creator)
        {
            this.Id = Id;
            this.Creator = Creator;
        }
        public ScheduleItem(ScheduleItemCRUDModel model)
        {
            this.Id = model.Id;
            this.Creator = int.Parse(model.Creator); // TODO: fix the inconsistency between datatypes
            this.Contact = model.Contact;
            this.DaysOfWeek = model.DaysOfWeek;
            this.EndTime = new TimeOnly(model.EndHour, model.EndMinute);
            this.Location = model.Location;
            this.Notes = model.Notes;
            this.StartTime = new TimeOnly(model.StartHour, model.StartMinute);
            this.Title = model.Title;
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
                    [ScheduleItemOptions.JsonHour] = StartTime.Hour,
                    [ScheduleItemOptions.JsonMinute] = StartTime.Minute
                },
                [ScheduleItemOptions.JsonEnd] = new JsonObject
                {
                    [ScheduleItemOptions.JsonHour] = EndTime.Hour,
                    [ScheduleItemOptions.JsonMinute] = EndTime.Minute
                },
                [ScheduleItemOptions.JsonLocation] = Location,
                [ScheduleItemOptions.JsonContact] = Contact,
                [ScheduleItemOptions.JsonNotes] = Notes,
                [ScheduleItemOptions.JsonCreator] = Creator
            };
            return result;
        }

        // Returns a shortened version of the string value, equal in length to the length argument.
        // Intended for use mainly in ScheduleItem.
        public string shorten(string value, int length)
        {
            if (value.Length <= length)
            {
                return value;
            }
            try
            {
                return value.Substring(0, length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
    }
}
