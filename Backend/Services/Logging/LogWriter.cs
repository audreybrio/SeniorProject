using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Marvel.Services.Logging
{
    // Writes logs to the console. Intended to be overridden by DbLogWriter &
    // FileLogWriter.
    public class LogWriter
    {
        private Queue<Log> logs { get; }
        public LogWriter()
        {
            logs = new Queue<Log>();
        }

        // Add a new log to the queue. Returns the number of logs successfully added.
        // Intended to be called with a discard, like so: _ = myLogWriter.AddLog(...);
        // Reference: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
        public virtual async Task<int> AddLog(string category, string level, int user, string description)
        {
            int result = 0;
            Log newLog = new Log(category, level, user, description);
            try
            {
                logs.Enqueue(newLog);
                result = await this.WriteAllLogs();
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception has occurred: " +
                                    ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        // Write all of the logs to the console.
        public virtual async Task<int> WriteAllLogs()
        {
            // If there are no logs, exit.
            if (logs.Count < 1)
            {
                return 0;
            }
            int result = 0;
            while (logs.Count > 0)
            {
                try
                {
                    Log current = logs.Dequeue();
                    result++;
                    Console.WriteLine(current.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The following exception has occurred: " +
                                        ex.GetType().FullName);
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }
            return result;
        }
    }

    // Writes logs to a database. The connection string must be configured as an environment variable,
    // or set after instantiation, otherwise this will fail.
    public class DbLogWriter : LogWriter
    {
        private Queue<Log> logs { get; }
        public string dbConnectionString { get; set; } = "";
        public string tableName { get; set; } = "logs";
        public DbLogWriter()
        {
            try
            {
                dbConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception has occurred: " +
                                        ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
            logs = new Queue<Log>();
        }
        // Add a new log to the queue. Returns the number of logs successfully added.
        // Intended to be called with a discard, like so: _ = myLogWriter.AddLog(...);
        // Reference: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
        public override async Task<int> AddLog(string category, string level, int user, string description)
        {
            int result = 0;
            Log newLog = new Log(category, level, user, description);
            try
            {
                logs.Enqueue(newLog);
                result = await this.WriteAllLogs();
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception has occurred: " +
                                    ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public override async Task<int> WriteAllLogs()
        {
            if (logs.Count < 1)
            {
                return 0;
            }
            int result = 0;
            while (logs.Count > 0)
            {
                int rowsAffected = 0;
                try
                {
                    Log current = logs.Dequeue();
                    using (SqlConnection connection = new SqlConnection(dbConnectionString))
                    {
                        string query = "INSERT INTO " + this.tableName + " (timestamp, category, level, username, description) "
                                                 + "VALUES (@timestamp, @category, @level, @username, @description);";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@timestamp", current.timestamp);
                            command.Parameters.AddWithValue("@category", current.category);
                            command.Parameters.AddWithValue("@layer", current.level);
                            command.Parameters.AddWithValue("@username", current.user);
                            command.Parameters.AddWithValue("@message", current.description);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The following exception has occurred: " +
                                        ex.GetType().FullName);
                    Console.WriteLine(ex.Message);
                }
                result += rowsAffected;
            }
            return result;
        }
    }

    public class FileLogWriter : LogWriter
    {
        private Queue<Log> logs { get; }
        public string filePath { get; set; }
        public FileLogWriter()
        {
            try
            {
                filePath = Environment.GetEnvironmentVariable("MARVELARCHIVEFILEPATH");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception has occurred: " +
                                        ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (filePath is null)
                {
                    filePath = "./archive.txt";
                }
            }
            logs = new Queue<Log>();
        }

        // Add a new log to the queue. Returns the number of logs successfully added.
        // Intended to be called with a discard, like so: _ = myLogWriter.AddLog(...);
        // Reference: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
        public override async Task<int> AddLog(string category, string level, int user, string description)
        {
            int result = 0;
            Log newLog = new Log(category, level, user, description);
            try
            {
                logs.Enqueue(newLog);
                result = await this.WriteAllLogs();
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception has occurred: " +
                                    ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        // Add an existing log to the queue. Returns the number of logs successfully added.
        public async Task<int> AddLog(string category, string level, int user, string description, DateTime timestamp)
        {
            int result = 0;
            Log newLog = new Log(category, level, user, description, timestamp);
            try
            {
                logs.Enqueue(newLog);
                result = await this.WriteAllLogs();
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception has occurred: " +
                                    ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public override async Task<int> WriteAllLogs()
        {
            if (logs.Count < 1)
            {
                return 0;
            }
            int logsWritten = 0;
            while (logs.Count > 0)
            {
                try
                {
                    Log current = logs.Dequeue();
                    File.AppendAllText(filePath, current.ToString() + "\n");
                    logsWritten++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The following exception has occurred: " +
                                        ex.GetType().FullName);
                    Console.WriteLine(ex.Message);
                }
            }
            return logsWritten;
        }
    }
}
