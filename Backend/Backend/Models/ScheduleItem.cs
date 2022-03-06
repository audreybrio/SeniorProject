using System.Text.Json.Nodes;

namespace StudentMultiTool.Backend.Models
{
    public class ScheduleItem
    {
        public static int maxContactLength { get; } = 64;
        public static int maxLocationLength { get; } = 64;
        public static int maxNotesLength { get; } = 64;
        public static int maxTitleLength { get; } = 64;
        public int Id { get; set; }
        public int Creator { get; set; }
        public string Contact
        {
            get
            {
                return Contact;
            }
            set
            {
                Contact = shorten((string)value, ScheduleItem.maxContactLength);
            }
        }
        public List<bool> DaysOfWeek { get; set; } = new List<bool> { false, false, false, false, false, false, false };
        public TimeOnly EndTime { get; set; } = new TimeOnly();
        public string Location
        {
            get
            {
                return this.Location;
            }
            set
            {
                this.Location = shorten((string)value, ScheduleItem.maxLocationLength);
            }
        }
        public string Notes
        {
            get
            {
                return Notes;
            }
            set
            {
                Notes = shorten((string)value, ScheduleItem.maxNotesLength);
            }
        }
        public TimeOnly StartTime { get; set; } = new TimeOnly();
        public string Title
        {
            get
            {
                return this.Title;
            }
            set
            {
                this.Title = shorten((string)value, ScheduleItem.maxTitleLength);
            }
        }
        public ScheduleItem(int Id, int Creator)
        {
            this.Id = Id;
            this.Creator = Creator;
        }
        public JsonObject ToJson()
        {
            // Good but might make more sense in the manager
            JsonObject result = new JsonObject
            {
                ["ScheduleItem" + Id.ToString()] = new JsonObject
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
                },
            };
            return result;
        }
        // Returns a shortened version of the string value, equal in length to the length argument.
        // Intended for use mainly in ScheduleItem.
        public string shorten(string value, int length)
        {
            try
            {
                return (string)value.Substring(0, length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
    }
}
