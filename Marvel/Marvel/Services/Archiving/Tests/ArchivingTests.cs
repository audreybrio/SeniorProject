using System;
using System.Data.SqlClient;
using Xunit;
using Marvel.Services;
using Marvel.Services.Logging;
using System.Collections.Generic;

namespace Tests
{
    public class ArchivingTests
    {
        [Fact]
        public void TestLogArchiverConstructor()
        {
            // Arrange
            LogArchiver testArchiver = null;

            // Act
            testArchiver = new LogArchiver();

            // Assert
            Assert.NotNull(testArchiver);
            Assert.NotNull(testArchiver.sql);
            Assert.NotNull(testArchiver._connection);
        }

        [Fact]
        public void TestReadFromDB()
        {
            // Arrange
            LogArchiver testArchiver = new LogArchiver();
            string connectionString = null;
            connectionString = Environment.GetEnvironmentVariable("MARVELTESTCONNECTIONSTRING");
            Assert.NotNull(connectionString);
            testArchiver._connection = new SqlConnection(connectionString);
            SqlDataReader testReader = null;

            // Act
            testReader = testArchiver.readFromDb(DateTime.UtcNow);

            // Assert
            Assert.NotNull(testReader);
        }

        [Fact]
        public void TestDeleteFromDB()
        {
            // Arrange
            string connectionString = Environment.GetEnvironmentVariable("MARVELTESTCONNECTIONSTRING");
            Assert.NotNull(connectionString);
            Assert.True(connectionString != "");

            string insertQuery = "INSERT INTO logs VALUES (@timestamp, @category, @level, @user, @description);";
            string selectQuery = "SELECT logs.id FROM logs WHERE (logs.timestamp >= @cutoff);";
            string testCategory = "TEST";
            string testLevel = "TEST";
            string testUser = "TestDeleteFromDB";
            string testDescription = "testLog";

            int limit = 5;
            int logsWritten = 0;

            // Set up test logs & cutoff
            string cutoff = DateTime.UtcNow.ToString();
            List<Log> logs = new List<Log>();
            for (int i = 0; i < limit; i++)
            {
                logs.Add(new Log(testCategory, testLevel, testUser, testDescription + i.ToString()));
            }

            // Write test logs to the database
            for (int i = 0; i < limit; i++)
            {
                Log current = logs[i];
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@timestamp", current.timestamp);
                            command.Parameters.AddWithValue("@category", current.category);
                            command.Parameters.AddWithValue("@level", current.level);
                            command.Parameters.AddWithValue("@user", current.user);
                            command.Parameters.AddWithValue("@description", current.description);
                            connection.Open();
                            logsWritten += command.ExecuteNonQuery();
                        }
                    }
                }
                catch { }
            }
            Assert.True(logsWritten > 0);

            // Get ids of test logs from database
            SqlDataReader idReader = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@cutoff", cutoff);
                        connection.Open();
                        idReader = command.ExecuteReader();
                    }
                }
            }
            catch { }
            Assert.NotNull(idReader);
            Assert.True(idReader.HasRows);
            List<int> ids = new List<int>();
            int index = 0;
            while (idReader.Read())
            {
                ids.Add(idReader.GetInt32(index));
                index++;
            }

            LogArchiver testArchiver = new LogArchiver();
            testArchiver._connection = new SqlConnection(connectionString);
            int result = -1;

            // Act
            result = testArchiver.deleteFromDB(ids);

            // Assert
            Assert.Equal(ids.Count, result);
        }
    }
}
