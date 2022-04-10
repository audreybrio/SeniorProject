using System.Data;
using System.Data.SqlClient;
using StudentMultiTool.Backend.Models.ScheduleBuilder;

namespace StudentMultiTool.Backend.Services.ScheduleBuilder
{
    public class ScheduleListBuilder
    {
        public string? dbConnectionString { get; set; } = null;
        public ScheduleListBuilder()
        {
            dbConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING", EnvironmentVariableTarget.User);
        }
        public ScheduleListBuilder(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }
        public async Task<IEnumerable<Schedule>> GetAllSchedulesForUser(string userHash)
        {
            if (this.dbConnectionString == null)
            {
                return Enumerable.Empty<Schedule>();
            }
            string scheduleListQuery = "SELECT Schedules.id, title, created, modified, path, canWrite, isOwner FROM Schedules " +
                                       "INNER JOIN Collaborators ON Schedules.id = schedule " +
                                       "INNER JOIN UserHashes ON collaborator = hash " +
                                       "WHERE collaborator = @collaborator";
            List<Schedule> result = new List<Schedule>();
            using (SqlConnection connection = new SqlConnection(this.dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(scheduleListQuery, connection))
                {
                    command.Parameters.AddWithValue("@collaborator", userHash);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            IDataRecord row = reader;
                            int? id = (int?)row[0];
                            string? title = (string?)row[1];
                            DateTime? created = (DateTime?)row[2];
                            DateTime? modified = (DateTime?)row[3];
                            string? path = (string?)row[4];

                            if (id != null && title != null &&
                                created != null && modified != null &&
                                !string.IsNullOrEmpty(path))
                            {
                                Schedule current = new Schedule(
                                    (int)id,
                                    -1,
                                    (DateTime)created,
                                    (DateTime)modified,
                                    (string)title,
                                    (string)path
                                );
                                result.Add(current);
                            }
                        }
                        connection.Close();
                    }
                    catch (System.InvalidOperationException ex)
                    {
                        Console.WriteLine("The following exception has occurred: " +
                                ex.GetType().FullName);
                        Console.WriteLine(ex.Message);
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("The following exception has occurred: " +
                                ex.GetType().FullName);
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return result.ToArray();
        }

        public string? GetUserHash(string username)
        {
            if (this.dbConnectionString == null)
            {
                throw new NullReferenceException();
            }
            string userHashQuery = "SELECT (hash) FROM UserHashes " +
                                   "INNER JOIN UserAccounts ON UserHashes.id = UserAccounts.id " +
                                   "WHERE username = @username";
            string? userHash = null;
            using (SqlConnection connection = new SqlConnection(this.dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(userHashQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            IDataRecord row = (IDataRecord)reader;
                            userHash = (string) row[0];
                        }
                        connection.Close();
                    }
                    catch (System.InvalidOperationException ex)
                    {
                        Console.WriteLine("The following exception has occurred: " +
                                ex.GetType().FullName);
                        Console.WriteLine(ex.Message);
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("The following exception has occurred: " +
                                ex.GetType().FullName);
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return userHash;
        }
    }
}
