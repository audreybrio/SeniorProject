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
        public async void TestAddLog()
        {
            // Arrange
            LogWriter logWriter = new LogWriter();
            string category = "User Review";
            string level = "Data Store";
            string user = "mkriesel";
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

            Task<int> result = logWriter.WriteAllLogs();

            Assert.Equal(1, await result);
        }

        [Fact]
        public async void TestDbAddLog()
        {
            DbLogWriter logWriter = new DbLogWriter();
            string category = "User Review";
            string level = "Data Store";
            string user = "mkriesel";
            string description = "User added new review";

            // Act
            Task<int> result = logWriter.AddLog(category, level, user, description);

            // Assert
            Assert.Equal(1, await result);
        }

        [Fact]
        public async void DbTestWriteAllLogs()
        {
            DbLogWriter logWriter = new DbLogWriter();

            Task<int> result = logWriter.WriteAllLogs();

            Assert.Equal(1, await result);
        }

        [Fact]
        public async void TestFileAddLog()
        {
            FileLogWriter logWriter = new FileLogWriter();
            string category = "User Review";
            string level = "Data Store";
            string user = "mkriesel";
            string description = "User added new review";

            // Act
            Task<int> result = logWriter.AddLog(category, level, user, description);

            // Assert
            Assert.Equal(1, await result);
        }

        [Fact]
        public async void DbFileWriteAllLogs()
        {
            FileLogWriter logWriter = new FileLogWriter();

            Task<int> result = logWriter.WriteAllLogs();

            Assert.Equal(1, await result);
        }
    }
}