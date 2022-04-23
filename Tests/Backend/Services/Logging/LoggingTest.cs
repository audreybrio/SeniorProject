using System;
using System.Data.SqlClient;
using Xunit;
using Marvel.Services;
using Marvel.Services.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class LoggingTest
    {
        [Fact]
        public async void Constructor()
        {
            DbLogWriter logWriter = new DbLogWriter();
            Assert.NotNull(logWriter);
            Assert.NotNull(logWriter.dbConnectionString);
            Assert.NotEqual(string.Empty, logWriter.dbConnectionString);

            // To really test this, you can uncomment these lines and replace the first string with the connection
            // string for your computer.
            //Assert.Equal("Data Source=LAPTOP-S1MIT52E\\SQLEXPRESS;Initial Catalog=Marvel;Integrated Security=True",
            //    logWriter.dbConnectionString);
            Console.WriteLine(logWriter.dbConnectionString);
        }
        [Fact]
        public async void TestAddLog()
        {
            // Arrange
            LogWriter logWriter = new LogWriter();
            string category = "User Review";
            string level = "Data Store";
            int user = 0;
            string description = "User added new review";
            
            // Act
            Task<int> result = logWriter.AddLog(category, level, user, description);

            // Assert
            Assert.Equal(1, await result);
        }

        [Fact]
        public async void TestWriteAllLogs()
        {
            LogWriter logWriter = new LogWriter();

            // Don't need to call AddLog() since we are unit testing WriteAllLogs()
            //Task<int> result = logWriter.AddLog("Test", "Test", 0, "TestWriteAllLogs()");
            Task<int> result = logWriter.WriteAllLogs();

            // Expected result should be 0
            Assert.Equal(0, await result);
        }

        [Theory]
        [InlineData("Test", "Test", 0, "TestDbAddLog()")]
        public async void TestDbAddLog(string category, string level, int user, string description)
        {
            DbLogWriter logWriter = new DbLogWriter();

            // Act
            //int? result = null;
            //result = await logWriter.AddLog(category, level, user, description);
            Task<int> t = logWriter.AddLog(category, level, user, description);
            int? result = (int) t.Result;

            // Assert
            // Test that logWriter initialized
            Assert.NotNull(logWriter);

            // Test that AddLog returned a task with a non-null result
            Assert.NotNull(result);

            // Test that result == 1
            Assert.Equal(1, (int) result);
        }

        [Theory]
        [InlineData("Test", "Test", 0, "TestDbAddLogWithDiscard()")]
        public async void TestDbAddLogWithDiscard(string category, string level, int user, string description)
        {
            DbLogWriter logWriter = new DbLogWriter();

            // Act
            _ = logWriter.AddLog(category, level, user, description);

            // Assert
            // Test that logWriter initialized
            Assert.NotNull(logWriter);
        }

        [Fact]
        public async void DbTestWriteAllLogs()
        {
            DbLogWriter logWriter = new DbLogWriter();

            Task<int> result = logWriter.WriteAllLogs();

            Assert.Equal(0, await result);
        }

        [Fact]
        public async void TestFileAddLog()
        {
            FileLogWriter logWriter = new FileLogWriter();
            string category = "User Review";
            string level = "Data Store";
            int user = 0;
            string description = "User added new review";

            // Act
            Task<int> result = logWriter.AddLog(category, level, user, description);

            // Assert
            Assert.Equal(0, await result);
        }

        [Fact]
        public async void DbFileWriteAllLogs()
        {
            FileLogWriter logWriter = new FileLogWriter();

            Task<int> result = logWriter.WriteAllLogs();

            Assert.Equal(0, await result);
        }
    }
}