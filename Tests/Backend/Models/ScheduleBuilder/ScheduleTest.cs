using System;
using System.Data.SqlClient;
using Xunit;
using StudentMultiTool.Backend.Services;
using StudentMultiTool.Backend.Services.Logging;
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
    }
}