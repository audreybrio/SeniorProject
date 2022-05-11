using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO.Compression;
using System.Threading;
using StudentMultiTool.Backend.Services.DataAccess;

namespace Marvel.Services.Logging
{
    public class LogArchiver
    {
        public string ConnectionString { get; set; }

        public LogArchiver()
        {
            ConnectionString = Environment.GetEnvironmentVariable(EnvironmentVariableEnum.CONNECTIONSTRING);
        }

        // Archive all logs older than 30 days
        public async void Archive()
        {
            // Get the first day of this month to establish the cutoff date.
            DateTime today = DateTime.Today;
            DateTime month = new DateTime(today.Year, today.Month, 1);
            DateTime first = month.AddMonths(-1);

            // Read the logs
            List<object[]> logs = readLogs(first);

            // Set up a list of Tasks (to archive each log)
            List<Task<int>> vs = new List<Task<int>>();

            // The list of ids of each log that is being archived
            List<int> ids = new List<int>();

            // Configure the log writer
            string filePath = Environment.GetEnvironmentVariable(EnvironmentVariableEnum.ARCHIVEFILEPATH);
            FileLogWriter fileLogWriter = new FileLogWriter();
            fileLogWriter.filePath = filePath;

            // Read each log after it's been retreived from the database
            foreach(object[] log in logs)
            {
                // Add the log's id to the list of archived logs
                ids.Add((int) log[0]);

                // Set up a task to write the log to the archive file
                vs.Add(fileLogWriter.AddLog(
                    (string) log[2],
                    (string) log[3],
                    (string) log[4],
                    (string) log[5],
                    (DateTime) log[1]
                    ));
            }

            // Archive the logs
            foreach (Task<int> current in vs)
            {
                await current;
            }

            _ = await fileLogWriter.WriteAllLogs();

            // Delete the archived logs
            deleteLogs(ids);

            // Compress the archive file
            string zipPath = Environment.GetEnvironmentVariable(EnvironmentVariableEnum.ARCHIVEZIPPATH);
            
            if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(zipPath))
            {
                // Try to compress the file.
                try
                {
                    ZipFile.CreateFromDirectory(filePath, zipPath);
                }
                catch (Exception ex)
                {

                }
            }

            // Activate the next archiving thread
            _ = await WaitForNextArchive();
        }

        // Waits for the 1st day of the next month to begin archiving again.
        public virtual async Task<int> WaitForNextArchive()
        {
            // Establish the next time to archive logs at.
            var current = DateTime.Now;
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1, 0, 0, 0);
            var first = month.AddMonths(+1);
            var restTime = first.Date - today;

            // Wait until the midnight on the 1st of the next month.
            Thread.Sleep(restTime);

            // Archive the logs
            Archive();

            // Exit
            return 0;
        }

        // Deletes all logs from the current set of logs that is being archived.
        public int deleteLogs(List<int> ids)
        {
            int result = -1;
            SqlCommandRunner runner = new SqlCommandRunner(ConnectionString);
            runner.Query = "DELETE FROM logs WHERE (logs.id IN @ids);";
            string logIds = string.Join(",", ids);
            runner.AddParam("@ids", logIds);
            result = runner.ExecuteNonQuery();
            return result;
        }

        // Reads all logs from before a cutoff (intended to be the set of all logs older than 30 days).
        public List<object[]> readLogs(DateTime cutoff)
        {
            List<object[]> logs = new List<object[]>();
            SqlCommandRunner runner = new SqlCommandRunner(ConnectionString);
            runner.Query = "SELECT * FROM logs WHERE (timestamp < @cutoff);";
            runner.AddParam("@cutoff", cutoff);
            logs = runner.ExecuteReader();
            return logs;
        }

    }
}
