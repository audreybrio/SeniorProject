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
    public class ScheduleListBuilderTest
    {
        private string testConnectionString = Environment.GetEnvironmentVariable("MARVELTESTCONNECTIONSTRING", EnvironmentVariableTarget.User);
        private string testUser = "";
        [Fact]
        public void Constructor1()
        {
            ScheduleListBuilder builder = new ScheduleListBuilder();
            Assert.NotNull(builder);
            Assert.NotNull(builder.dbConnectionString);
        }
        [Fact]
        public void Constructor2()
        {
            ScheduleListBuilder builder = new ScheduleListBuilder(testConnectionString);
            Assert.NotNull(builder);
            Assert.NotNull(builder.dbConnectionString);
            Assert.Equal(testConnectionString, builder.dbConnectionString);
        }
        [Fact]
        public async void GetAllSchedulesForUser()
        {
            ScheduleListBuilder builder = new ScheduleListBuilder(testConnectionString);
            IEnumerable<Schedule> results = await builder.GetAllSchedulesForUser(testUser);
            Assert.NotNull(results);
        }
    }
}
