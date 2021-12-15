using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO.Compression;

namespace Marvel.Services.Logging
{
    public class LogArchiver
    {
        private static string sql { get; set; }

        public static SqlConnection _connection { get; set; }

        public LogArchiver()
        {
            sql = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            _connection = new SqlConnection(sql);
        }

        public async void run()
        {
            DateTime today = DateTime.Today;
            DateTime month = new DateTime(today.Year, today.Month, 1);
            DateTime first = month.AddMonths(-1);
            SqlDataReader reader = readFromDb(first);

            List<Task<int>> vs = new List<Task<int>>();

            List<int> ids = new List<int>();

            FileLogWriter fileLogWriter = new FileLogWriter();
            while(reader.Read())
            {
                ids.Add(reader.GetInt32(0));
                vs.Add(fileLogWriter.AddLog(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(1)));
            }

            foreach (Task<int> current in vs)
            {
                await current;
            }

            fileLogWriter.WriteAllLogs();

            deleteFromDB(ids);

            string filePath = Environment.GetEnvironmentVariable("MARVELARCHIVEFILEPATH");
            string zipPath = Environment.GetEnvironmentVariable("MARVELARCHIVEZIPPATH");

            ZipFile.CreateFromDirectory(filePath, zipPath);

            nextArchive(); // Activate next thread
        }

        public virtual async Task<int> nextArchive()
        {
            var current = DateTime.Now;
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month.AddMonths(+1);
            var restTime = first.Date - today;
            Thread.Sleep(restTime);

            run();

            return 0;
        }

        public void deleteFromDB(List<int> ids)
        {
            string query = "DELETE FROM logs WHERE (logs.id IN " + String.Join(",", ids) + ")";
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.ExecuteReader();
        }

        public SqlDataReader readFromDb(DateTime cutoff)
        {
            string query = "SELECT * FROM logs WHERE (timestamp < " + cutoff.ToString() + ");";
            SqlCommand cmd = new SqlCommand(query, _connection);

            return cmd.ExecuteReader();
        }

    }
}
