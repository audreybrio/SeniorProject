using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleBuilder;
using System.Text.Json.Nodes;

namespace Tests
{
    public class ScheduleFileAccessorTest
    {
        public static string baseFilePath = "../smt-storage/testSchedules/";
        [Fact]
        public async void Constructor()
        {
            // Make two ScheduleFileAccessors, one with and one without indentation.
            ScheduleFileAccessor testWithIndentation = new ScheduleFileAccessor(true);
            ScheduleFileAccessor testNoIndentation = new ScheduleFileAccessor(false);

            // Check that both ScheduleFileAccessors aren't null
            Assert.NotNull(testWithIndentation);
            Assert.NotNull(testNoIndentation);

            // Check that their indentation values are correct
            Assert.True(testWithIndentation.Indentation);
            Assert.False(testNoIndentation.Indentation);
        }
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void WriteScheduleItems(bool indentation)
        {
            // Set up a test Schedule
            string path = "./ScheduleFileAccessorTest.WriteScheduleItems.json";
            Schedule testSchedule = new Schedule(0, 0, DateTime.Now, DateTime.Now, "Spring 2022", path);

            // Set up a bunch of ScheduleItems as test data
            ScheduleItem a = new ScheduleItem(0, 0);
            a.Title = "CECS 123";
            a.StartTime = new TimeOnly(12, 30);
            a.EndTime = new TimeOnly(13, 45);
            a.Location = "ECS 301";
            a.Contact = "Some Body";
            a.DaysOfWeek[2] = true;
            a.DaysOfWeek[4] = true;

            ScheduleItem b = new ScheduleItem(1, 0);
            b.Title = "ENGR 300";
            b.StartTime = new TimeOnly(17, 30);
            b.EndTime = new TimeOnly(18, 45);
            b.Location = "VEC 333";
            b.Contact = "S. Body";
            b.DaysOfWeek[1] = true;
            b.DaysOfWeek[3] = true;

            ScheduleItem c = new ScheduleItem(2, 0);
            c.Title = "CECS 451";
            c.StartTime = new TimeOnly(14, 30);
            c.EndTime = new TimeOnly(15, 45);
            c.Location = "ECS 408";
            c.Contact = "Some BodyElse";
            c.DaysOfWeek[2] = true;
            c.DaysOfWeek[4] = true;

            ScheduleItem d = new ScheduleItem(3, 0);
            d.Title = "CECS 491B";
            d.StartTime = new TimeOnly(12, 30);
            d.EndTime = new TimeOnly(13, 45);
            d.Location = "VEC 212";
            d.Contact = "S.B. Else";
            d.DaysOfWeek[1] = true;
            d.DaysOfWeek[3] = true;

            // Add the ScheduleItems to the Schedule
            testSchedule.AddScheduleItem(a);
            testSchedule.AddScheduleItem(b);
            testSchedule.AddScheduleItem(c);
            testSchedule.AddScheduleItem(d);

            // Create a ScheduleFileAccessor
            ScheduleFileAccessor accessor = new ScheduleFileAccessor(indentation);

            // Write the ScheduleItems to a file
            string result = accessor.WriteScheduleItems(testSchedule, baseFilePath);

            // If the file was written to without exception, then the test should pass
            Assert.Equal(ScheduleFileAccessor.Success, result);
        }
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void ReadScheduleItems(bool indentation)
        {
            // Set up a test Schedule
            string path = "./ScheduleFileAccessorTest.ReadScheduleItems.json";
            Schedule testSchedule = new Schedule(0, 0, DateTime.Now, DateTime.Now, "Spring 2022", path);

            // Set up a bunch of ScheduleItems as test data
            ScheduleItem a = new ScheduleItem(0, 0);
            a.Title = "CECS 123";
            a.StartTime = new TimeOnly(12, 30);
            a.EndTime = new TimeOnly(13, 45);
            a.Location = "ECS 301";
            a.Contact = "Some Body";
            a.DaysOfWeek[2] = true;
            a.DaysOfWeek[4] = true;

            ScheduleItem b = new ScheduleItem(1, 0);
            b.Title = "ENGR 300";
            b.StartTime = new TimeOnly(17, 30);
            b.EndTime = new TimeOnly(18, 45);
            b.Location = "VEC 333";
            b.Contact = "S. Body";
            b.DaysOfWeek[1] = true;
            b.DaysOfWeek[3] = true;

            ScheduleItem c = new ScheduleItem(2, 0);
            c.Title = "CECS 451";
            c.StartTime = new TimeOnly(14, 30);
            c.EndTime = new TimeOnly(15, 45);
            c.Location = "ECS 408";
            c.Contact = "Some BodyElse";
            c.DaysOfWeek[2] = true;
            c.DaysOfWeek[4] = true;

            ScheduleItem d = new ScheduleItem(3, 0);
            d.Title = "CECS 491B";
            d.StartTime = new TimeOnly(12, 30);
            d.EndTime = new TimeOnly(13, 45);
            d.Location = "VEC 212";
            d.Contact = "S.B. Else";
            d.DaysOfWeek[1] = true;
            d.DaysOfWeek[3] = true;

            List<ScheduleItem> testItems = new List<ScheduleItem>
            {
                a,
                b,
                c,
                d
            };

            // Add the ScheduleItems to the Schedule
            foreach (ScheduleItem si in testItems)
            {
                testSchedule.AddScheduleItem(si);
            }

            // Create a ScheduleFileAccessor
            ScheduleFileAccessor accessor = new ScheduleFileAccessor(indentation);

            // Write the ScheduleItems to a file
            accessor.WriteScheduleItems(testSchedule, baseFilePath);
            
            // Read them from the files
            List<ScheduleItem> itemsWithIndentation = accessor.ReadScheduleItems(path);

            // Test data and file contents should have the same count
            Assert.Equal(testItems.Count, itemsWithIndentation.Count);

            // Check that the ScheduleItem data was read correctly
            int titlesCorrectlyRead = 0;
            int contactsCorrectlyRead = 0;
            int locationsCorrectlyRead = 0;
            foreach (ScheduleItem expected in testItems)
            {
                foreach (ScheduleItem actual in itemsWithIndentation)
                {
                    if (expected.Title.Equals(actual.Title))
                    {
                        titlesCorrectlyRead++;
                    }
                    if (expected.Contact.Equals(actual.Contact))
                    {
                        contactsCorrectlyRead++;
                    }
                    if (expected.Location.Equals(actual.Location))
                    {
                        locationsCorrectlyRead++;
                    }
                }
            }

            Assert.Equal(testItems.Count, titlesCorrectlyRead);
            Assert.Equal(testItems.Count, contactsCorrectlyRead);
            Assert.Equal(testItems.Count, locationsCorrectlyRead);
        }
    }
}
