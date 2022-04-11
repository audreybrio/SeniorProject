using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using System.Text.Json.Nodes;

namespace Tests
{
    public class ScheduleItemTest
    {
        [Fact]
        public async void Constructor1()
        {
            int testId = 1;
            int testCreator = 2;
            ScheduleItem testItem = new ScheduleItem(testId, testCreator);

            Assert.NotNull(testItem);
            Assert.NotEqual(testItem.Id, testItem.Creator);
            Assert.Equal(testId, testItem.Id);
            Assert.Equal(testCreator, testItem.Creator);
        }
        [Fact]
        public async void ConstructorFromJson()
        {
            // Create a JsonObject and feed it into the constructor
            ScheduleItem testItem = new ScheduleItem(0, 0);
            Assert.NotNull(testItem);
        }
        [Fact]
        public async void Shorten()
        {
            // Id, Creator, and most other fields don't matter for this test
            // We just need to set an arbitrary string attribute and make sure
            // ScheduleItem.shorten() is working properly
            ScheduleItem testItem = new ScheduleItem(0, 0);
            string testString = "Hello world!";

            // Act: call shorten() by setting giving the Notes attribute a value
            testItem.Notes = testString;
            string result = testItem.Notes;

            // Neither testItem.Notes or the result should be empty
            Assert.NotEqual(testItem.Notes, string.Empty);
            Assert.NotEqual(result, string.Empty);

            // testString and result should be same lengths and same strings
            Assert.Equal(testString.Length, result.Length);
            Assert.Equal(testString, result);
        }
        [Fact]
        public async void ToJson()
        {
            // Set up a testItem so we can use ScheduleItem.ToJson()
            ScheduleItem testItem = new ScheduleItem(0, 0);

            // Give it some sample data
            testItem.Notes = "Some notes";
            testItem.Contact = "Somebody";
            testItem.Location = "Somewhere";
            testItem.Title = "Something";
            testItem.StartTime = new TimeOnly(12, 30);
            testItem.EndTime = new TimeOnly(13, 31);
            testItem.DaysOfWeek = new List<bool>
            {
                true,
                false,
                true,
                false,
                false,
                false,
                false
            };

            // Call ToJson()
            JsonObject testJson = testItem.ToJson();

            // Make sure the data is still correct! If all of the data matches,
            // then the test should pass.

            // General information
            Assert.Equal(testItem.Notes, (String) testJson["notes"]);
            Assert.Equal(testItem.Contact, (String) testJson["contact"]);
            Assert.Equal(testItem.Location, (String) testJson["location"]);
            Assert.Equal(testItem.Title, (String) testJson["title"]);

            // Start & end times
            Assert.Equal(testItem.StartTime.Hour, (int) testJson["start"]["hour"]);
            Assert.Equal(testItem.StartTime.Minute, (int) testJson["start"]["minute"]);
            Assert.Equal(testItem.EndTime.Hour, (int) testJson["end"]["hour"]);
            Assert.Equal(testItem.EndTime.Minute, (int) testJson["end"]["minute"]);

            // Days of week
            Assert.Equal(testItem.DaysOfWeek[0], (bool) testJson["days"]["sun"]);
            Assert.Equal(testItem.DaysOfWeek[1], (bool) testJson["days"]["mon"]);
            Assert.Equal(testItem.DaysOfWeek[2], (bool) testJson["days"]["tue"]);
            Assert.Equal(testItem.DaysOfWeek[3], (bool) testJson["days"]["wed"]);
            Assert.Equal(testItem.DaysOfWeek[4], (bool) testJson["days"]["thu"]);
            Assert.Equal(testItem.DaysOfWeek[5], (bool) testJson["days"]["fri"]);
            Assert.Equal(testItem.DaysOfWeek[6], (bool) testJson["days"]["sat"]);
        }
    }
}
