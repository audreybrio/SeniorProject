using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentMultiTool.Backend.Models.ScheduleBuilder;

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

            // testString and result should be different lengths and different strings
            Assert.NotEqual(testString.Length, result.Length);
            Assert.NotEqual(testString, result);
        }
    }
}
