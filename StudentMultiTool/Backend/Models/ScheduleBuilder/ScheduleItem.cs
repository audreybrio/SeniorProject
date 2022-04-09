using System.Text.Json.Nodes;

namespace StudentMultiTool.Backend.Models.ScheduleBuilder
{
    // Represents a single, reoccurring daily or weekly event
    public class ScheduleItem
    {
        // Options for all ScheduleItems. These are maximum
        // string lengths, intended to preserve storage space.
        // See ScheduleItem.shorten() for usage.
        public static int maxContactLength { get; } = 64;
        public static int maxLocationLength { get; } = 64;
        public static int maxNotesLength { get; } = 64;
        public static int maxTitleLength { get; } = 64;

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
                _Contact = shorten(value, ScheduleItem.maxContactLength);
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
                _Location = shorten(value, ScheduleItem.maxLocationLength);
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
                _Notes = shorten(value, ScheduleItem.maxNotesLength);
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
                _Title = shorten(value, ScheduleItem.maxTitleLength);
            }
        }

        public ScheduleItem(int Id, int Creator)
        {
            this.Id = Id;
            this.Creator = Creator;
        }

        // Returns the ScheduleItem as a JsonObject.
        public JsonObject ToJson()
        {
            JsonObject result = new JsonObject
            {
                ["title"] = Title,
                ["days"] = new JsonObject
                {
                    ["sun"] = DaysOfWeek[0],
                    ["mon"] = DaysOfWeek[1],
                    ["tue"] = DaysOfWeek[2],
                    ["wed"] = DaysOfWeek[3],
                    ["thu"] = DaysOfWeek[4],
                    ["fri"] = DaysOfWeek[5],
                    ["sat"] = DaysOfWeek[6]
                },
                ["start"] = new JsonObject
                {
                    ["hour"] = StartTime.Hour,
                    ["minute"] = StartTime.Minute
                },
                ["end"] = new JsonObject
                {
                    ["hour"] = EndTime.Hour,
                    ["minute"] = EndTime.Minute
                },
                ["location"] = Location,
                ["contact"] = Contact,
                ["notes"] = Notes,
                ["creator"] = Creator
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
