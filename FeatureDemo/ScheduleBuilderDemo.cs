using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentMultiTool.Backend.Models.ScheduleBuilder;

namespace ConsoleDemo
{
    public class ScheduleBuilderDemo
    {
        public ScheduleBuilderDemo()
        { }

        public void Demo()
        {
            // Set schedule info
            string title = "Demo";
            string path = @"C:\Users\Public\scheduleBuilderDemo.json";
            int demoId = 0;

            Schedule demoSchedule = new Schedule(0, demoId, DateTime.Now, DateTime.Now, title, path);
            ScheduleItem demoItem = new ScheduleItem(0, demoId);
            
            // Set the demo item to occur on Monday and Wednesday from 5:30 PM - 6:45 PM
            demoItem.DaysOfWeek[1] = true;
            demoItem.DaysOfWeek[3] = true;
            demoItem.StartTime = new TimeOnly(17, 30);
            demoItem.EndTime = new TimeOnly(18, 45);

            // Add some other data
            demoItem.Title = "CECS 491B";
            demoItem.Location = "DESN 112";
            demoItem.Contact = "Vatanak Vong";
            demoItem.Notes = "ScheduleItems can represent classes/shifts/commutes/anything!";

            // Set up a commute
            ScheduleItem commuteTo = new ScheduleItem(1, demoId);
            commuteTo.Title = "To campus";
            commuteTo.DaysOfWeek = demoItem.DaysOfWeek;
            commuteTo.StartTime = new TimeOnly(16, 0);
            commuteTo.EndTime = new TimeOnly(17, 0);

            // Both ways!
            ScheduleItem commuteFrom = new ScheduleItem(2, demoId);
            commuteFrom.Title = "From campus";
            commuteFrom.DaysOfWeek = demoItem.DaysOfWeek;
            commuteFrom.StartTime = new TimeOnly(19, 0);
            commuteFrom.EndTime = new TimeOnly(19, 30);

            // Add them all to the schedule
            demoSchedule.AddScheduleItem(commuteTo);
            demoSchedule.AddScheduleItem(demoItem);
            demoSchedule.AddScheduleItem(commuteFrom);

            // Write the ScheduleItems to a file.
            Console.WriteLine("Writing schedule to " + path + " ...");
            ScheduleFileAccessor readWriter = new ScheduleFileAccessor(true);
            string result = readWriter.WriteScheduleItems(demoSchedule);
            Console.WriteLine(result);
            if (result.Equals(ScheduleFileAccessor.Success))
            {
                Console.WriteLine("Successfully written schedule to " + path);
            }
            else
            {
                Console.WriteLine("Error: " + result);
            }

            // Read the ScheduleItems from a file.
            path = @"C:\Users\Public\scheduleBuilderDemo.json";

            Console.WriteLine("Reading schedule from " + path + " ...");
            List<ScheduleItem> fromFile = readWriter.ReadScheduleItems(path);
            foreach (ScheduleItem si in fromFile)
            {
                Console.WriteLine(si.ToJson());
            }
            Console.WriteLine("End of file\n");
        }
    }
}
