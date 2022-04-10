using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleBuilder;
using System.Text.Json.Nodes;

namespace Tests.Backend.Services.ScheduleBuilder
{
    public class ScheduleManagerTest
    {
        private string testConnectionString = Environment.GetEnvironmentVariable("MARVELTESTCONNECTIONSTRING", EnvironmentVariableTarget.User);
        private string testBaseFilePath = "./";

        [Fact]
        public void ScheduleManagerConstructor1()
        {
            ScheduleManager manager = new ScheduleManager();
            Assert.NotNull(manager);
            Assert.NotNull(manager.dbConnectionString);
            Assert.NotNull(manager.BaseFilePath);
        }
        [Fact]
        public void ScheduleManagerConstructor2()
        {
            ScheduleManager manager = new ScheduleManager(testConnectionString, testBaseFilePath);
            Assert.NotNull(manager);
            Assert.NotNull(manager.dbConnectionString);
            Assert.NotNull(manager.BaseFilePath);
            Assert.Equal(testConnectionString, manager.dbConnectionString);
            Assert.Equal(testBaseFilePath, manager.BaseFilePath);
        }
        [Fact]
        public void InsertSchedule()
        {
            Schedule testSchedule = new Schedule
            (
                123,
                DateTime.Now,
                DateTime.Now,
                "ScheduleManagerTest.testSchedule",
                "ScheduleManagerTest." + DateTime.Now.ToString() + ".json"
            );
            ScheduleManager manager = new ScheduleManager(testConnectionString, testBaseFilePath);

            // Since this method returns the ID of the resulting schedule if successful,
            // we can only check that result is not null. We won't necessarily know what ID
            // the database will assign testSchedule once it's in the database.
            int? result = manager.InsertSchedule(testSchedule);
            Assert.NotNull(result);
        }
        [Fact]
        public void InsertCollaborator()
        {
            // Set up for the actual test: skip to after the call to InsertSchedule()
            Schedule testSchedule = new Schedule
            (
                123,
                DateTime.Now,
                DateTime.Now,
                "ScheduleManagerTest.testSchedule",
                "ScheduleManagerTest." + DateTime.Now.ToString() + ".json"
            );
            ScheduleManager manager = new ScheduleManager(testConnectionString, testBaseFilePath);

            // Since this method returns the ID of the resulting schedule if successful,
            // we can only check that result is not null. We won't necessarily know what ID
            // the database will assign testSchedule once it's in the database.
            int? testId = manager.InsertSchedule(testSchedule);
            Assert.NotNull(testId);

            string userHash = "";
            
            if (testId != null)
            {
                // Now the real test: Insert the user as a collaborator. If successful, exactly
                // 1 row should be updated.
                int rowsAffected = manager.InsertCollaborator((int) testId, userHash, true, true);
                Assert.Equal(1, rowsAffected);
            }
        }
        [Fact]
        public void DeleteSchedule()
        {
            // Set up for the actual test: skip to after the call to InsertSchedule() & Assert.NotNull()
            Schedule testSchedule = new Schedule
            (
                123,
                DateTime.Now,
                DateTime.Now,
                "ScheduleManagerTest.testSchedule",
                "ScheduleManagerTest." + DateTime.Now.ToString() + ".json"
            );
            ScheduleManager manager = new ScheduleManager(testConnectionString, testBaseFilePath);

            // Since this method returns the ID of the resulting schedule if successful,
            // we can only check that result is not null. We won't necessarily know what ID
            // the database will assign testSchedule once it's in the database.
            int? testId = manager.InsertSchedule(testSchedule);
            Assert.NotNull(testId);

            if (testId != null)
            {
                // Now the real test: Delete the schedule. If successful, exactly
                // 1 row should be updated.
                int rowsAffected = manager.DeleteSchedule((int)testId);
                Assert.Equal(1, rowsAffected);
            }
        }
        [Fact]
        public void SelectScheduleWithoutItems()
        {
            // Set up for the actual test: skip to after the call to InsertSchedule() & Assert.NotNull()
            Schedule testSchedule = new Schedule
            (
                123,
                DateTime.Now,
                DateTime.Now,
                "ScheduleManagerTest.testSchedule",
                "ScheduleManagerTest." + DateTime.Now.ToString() + ".json"
            );
            ScheduleManager manager = new ScheduleManager(testConnectionString, testBaseFilePath);

            // Since this method returns the ID of the resulting schedule if successful,
            // we can only check that result is not null. We won't necessarily know what ID
            // the database will assign testSchedule once it's in the database.
            int? testId = manager.InsertSchedule(testSchedule);
            Assert.NotNull(testId);

            if (testId != null)
            {
                // Now the real test: get the schedule
                Schedule? result = manager.SelectScheduleWithoutItems((int)testId);

                // Check that the selected schedule's properties match those we inserted.
                Assert.NotNull(result);
                Assert.Equal(testSchedule.OwnerId, result.OwnerId);
                Assert.Equal(testSchedule.Title, result.Title);
                Assert.Equal(testSchedule.Created, result.Created);
                Assert.Equal(testSchedule.Modified, result.Modified);
                Assert.Equal(testSchedule.Path, result.Path);
                Assert.Equal(testSchedule.Items.Count, result.Items.Count);
                Assert.Equal(testSchedule.Items, result.Items);
            }
        }
        [Fact]
        public void SelectScheduleWithItems()
        {
            // Set up for the actual test: skip to after the call to InsertSchedule() & Assert.NotNull()
            Schedule testSchedule = new Schedule
            (
                123,
                DateTime.Now,
                DateTime.Now,
                "ScheduleManagerTest.testSchedule",
                "ScheduleManagerTest." + DateTime.Now.ToString() + ".json"
            );
            ScheduleManager manager = new ScheduleManager(testConnectionString, testBaseFilePath);
            int id = 1;
            int creator = 123;
            ScheduleItem testItem = new ScheduleItem(id, creator);
            testItem.Title = "ScheduleManagerTest.SelectScheduleWithItems";
            testItem.StartTime = new TimeOnly(12, 13);
            testItem.EndTime = new TimeOnly(14, 15);
            testItem.DaysOfWeek[0] = true;
            testItem.DaysOfWeek[1] = true;
            testItem.DaysOfWeek[2] = true;
            testItem.DaysOfWeek[4] = true;
            testItem.Contact = "c";
            testItem.Location = "l";
            testItem.Notes = "n";

            // Since this method returns the ID of the resulting schedule if successful,
            // we can only check that result is not null. We won't necessarily know what ID
            // the database will assign testSchedule once it's in the database.
            int? testId = manager.InsertSchedule(testSchedule);
            Assert.NotNull(testId);

            if (testId != null)
            {
                // Now the real test: get the schedule
                Schedule? result = manager.SelectScheduleWithItems((int)testId);

                // Check that the selected schedule's properties match those we inserted.
                Assert.NotNull(result);
                Assert.Equal(testSchedule.OwnerId, result.OwnerId);
                Assert.Equal(testSchedule.Title, result.Title);
                Assert.Equal(testSchedule.Created, result.Created);
                Assert.Equal(testSchedule.Modified, result.Modified);
                Assert.Equal(testSchedule.Path, result.Path);
                Assert.Equal(testSchedule.Items.Count, result.Items.Count);
                Assert.Equal(testSchedule.Items, result.Items);
            }
        }
    }
}
