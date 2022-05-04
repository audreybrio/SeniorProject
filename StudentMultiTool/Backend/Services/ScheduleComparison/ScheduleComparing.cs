namespace StudentMultiTool.Backend.Services.ScheduleComparison
{
    public class ScheduleComparing
    {
        // Gets overlap of time for two users 
        public static string GetOverlap(string username, int id)
        {
            if (id == 1) { return "MW 10am - 2pm, TTH 12pm-5pm; 8pm - 10pm, F 9am - 1pm"; }
            else if (id == 2) { return "MW 4pm - 12am, TTH 3pm-7pm"; }
            else if (id == 3) { return "MW 11am - 3pm, TTH 1pm-5pm; 7pm - 9pm, F 9am - 12pm"; }
            else if (id == 4) { return "MW 12pm - 2pm; 4pm - 9pm, TTH 2pm-6pm, F 10am - 4pm"; }
            else if (id == 5) { return "MW 4pm - 9pm, TTH 12pm-5pm; 7pm - 9pm, F 11am - 3pm"; }
            else if (id == 6) { return "MW 5:30pm - 8am, TTH 4pm-9pm"; }
            else if (id == 7) { return "MW 4pm - 12am, TTH 3pm-7pm, F 3pm - 8pm"; }
            else { return "No overlap"; }
        }
    }
}
