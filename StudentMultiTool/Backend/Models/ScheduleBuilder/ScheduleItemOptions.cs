namespace StudentMultiTool.Backend.Models.ScheduleBuilder
{
    public class ScheduleItemOptions
    {
        // Options for all ScheduleItems. These are maximum
        // string lengths, intended to preserve storage space.
        // See ScheduleItem.shorten() for usage.
        // TODO: Move the hardcoded values into a config file.
        public static int maxContactLength { get; } = 64;
        public static int maxLocationLength { get; } = 64;
        public static int maxNotesLength { get; } = 64;
        public static int maxTitleLength { get; } = 64;

        // Default JSON keys for all ScheduleItems.
        // See ScheduleItem.ToJson() for usage.
        // TODO: Move the hardcoded values into a config file.
        public static string JsonTitle { get; } = "title";
        public static string JsonDays { get; } = "days";
        public static string JsonSunday { get; } = "sun";
        public static string JsonMonday { get; } = "mon";
        public static string JsonTuesday { get; } = "tue";
        public static string JsonWednesday { get; } = "wed";
        public static string JsonThursday { get; } = "thu";
        public static string JsonFriday { get; } = "fri";
        public static string JsonSaturday { get; } = "sat";
        public static string JsonStart { get; } = "start";
        public static string JsonHour { get; } = "hour";
        public static string JsonMinute { get; } = "minute";
        public static string JsonEnd { get; } = "end";
        public static string JsonLocation { get; } = "location";
        public static string JsonContact { get; } = "contact";
        public static string JsonNotes { get; } = "notes";
        public static string JsonCreator { get; } = "creator";
        public static string JsonId { get; } = "id";
        // Keys & values used in CRUD operations from web requests.
        // Pairs send by web requests should look like operation:create, etc.
        // Key
        public static string item { get; } = "item";
        // Key
        public static string operation { get; } = "op";
        // Value
        public static string create { get; } = "create";
        // Value
        public static string update { get; } = "update";
        // Value
        public static string delete { get; } = "delete";
        public static List<string> CRUD { get; } = new List<string>
        {
            create, update, delete
        };
    }
}
