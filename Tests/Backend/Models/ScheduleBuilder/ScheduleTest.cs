using System;
using System.Data.SqlClient;
using Xunit;
using Marvel.Services;
using Marvel.Services.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentMultiTool.Backend.Models.ScheduleBuilder;

namespace Tests
{
    public class ScheduleTest
    {
        [Fact]
        public async void Constructor1()
        {
            // Arrange
            int ownerId = 0;
            DateTime created = DateTime.Now;
            DateTime modified = DateTime.Now;
            String title = "TestSchedule";
            String path = "./" + title + "Constructor1.json";

            // Act
            Schedule test = new Schedule(ownerId, created, modified, title, path);

            // Assert
            Assert.NotNull(test);
            Assert.Equal(ownerId, test.OwnerId);
            Assert.Equal(created, test.Created);
            Assert.Equal(modified, test.Modified);
            Assert.Equal(title, test.Title);
        }

        [Fact]
        public async void Constructor2()
        {
            // Arrange
            int ownerId = 1;
            int scheduleId = 1;
            DateTime created = DateTime.Now;
            DateTime modified = DateTime.Now;
            String title = "TestSchedule";
            String path = "./" + title + "Constructor2.json";

            // Act
            Schedule test = new Schedule(scheduleId, ownerId, created, modified, title, path);

            // Assert
            Assert.NotNull(test);
            Assert.Equal(scheduleId, test.Id);
            Assert.Equal(ownerId, test.OwnerId);
            Assert.Equal(created, test.Created);
            Assert.Equal(modified, test.Modified);
            Assert.Equal(title, test.Title);
        }

        // Schedule.AddScheduleItem() should not fail for any small n.
        // This particular test would fail if n >= 0, but in those cases,
        // AddScheduleItem() would not even be called in this test. Therefore, 
        // those cases are ignored.
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(16)]
        [InlineData(32)]
        [InlineData(35)]
        [InlineData(64)]
        public async void AddScheduleItem(int n)
        {
            // Arrange
            int ownerId = 1;
            int scheduleId = 1;
            DateTime created = DateTime.Now;
            DateTime modified = DateTime.Now;
            String title = "TestSchedule";
            String path = "./" + title + "AddScheduleItem.json";
            Schedule test = new Schedule(scheduleId, ownerId, created, modified, title, path);

            // Get the initial number of ScheduleItems (0)
            int initalCount = test.Items.Count;
            
            // Act
            for (int i = 0; i < n; i++)
            {
                test.AddScheduleItem(new ScheduleItem(i, ownerId));
            }

            // Get the final number of ScheduleItems (in theory, n)
            int finalCount = test.Items.Count;

            // Assert
            // The initial count should not be equal to the final count, since AddScheduleItem
            // should increase the count by 1 for each call
            Assert.NotEqual(initalCount, finalCount);

            // The final count should be equal to n, since AddScheduleItem was called n times.
            Assert.Equal(finalCount, n);
        }
    }
}