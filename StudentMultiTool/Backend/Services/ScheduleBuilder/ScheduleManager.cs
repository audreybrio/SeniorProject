using System.Data;
using System.Data.SqlClient;
using StudentMultiTool.Backend.Models.ScheduleBuilder;

namespace StudentMultiTool.Backend.Services.ScheduleBuilder
{
    // Manages CRUD operations for Schedules.
    public class ScheduleManager
    {
        public string? dbConnectionString { get; set; } = null;
        private string _baseFilePath = string.Empty;
        private ScheduleFileAccessor _fileAccessor = new ScheduleFileAccessor();
        public string BaseFilePath
        {
            get
            {
                return _baseFilePath;
            }
            // Ensure that ScheduleManager.BaseFilePath
            set
            {
                _baseFilePath = (string)value;
                int lastIndex = value.Length - 1;
                if (!_baseFilePath[lastIndex].Equals("/"))
                {
                    _baseFilePath = value + "/";
                }
                else
                {
                    _baseFilePath = value;
                }
            }
        }
        public ScheduleManager()
        {
            dbConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING", EnvironmentVariableTarget.User);
            BaseFilePath = "./";
        }
        public ScheduleManager(string dbConnectionString, string BaseFilePath)
        {
            this.dbConnectionString = dbConnectionString;
            this.BaseFilePath = BaseFilePath;
        }

        // Create a schedule and return its id if successful.
        public int? InsertSchedule(Schedule newSchedule)
        {
            if (this.dbConnectionString == null)
            {
                throw new NullReferenceException();
            }
            string scheduleQuery = "INSERT INTO Schedules (title, created, modified, path) " +
                                   "VALUES (@title, @created, @modified, @path);";
            int? newId = null;
            string scheduleIDQuery = "SELECT id FROM Schedules WHERE path = @path;";
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(scheduleQuery, connection))
                {
                    command.Parameters.AddWithValue("@title", newSchedule.Title);
                    command.Parameters.AddWithValue("@created", newSchedule.Created);
                    command.Parameters.AddWithValue("@modified", newSchedule.Modified);
                    command.Parameters.AddWithValue("@path", newSchedule.Path);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
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

                if (rowsAffected == 1)
                {
                    // Get the ID of newSchedule in the db
                    Console.WriteLine("Get the ID of newSchedule in the db");
                    using (SqlCommand command = new SqlCommand(scheduleIDQuery, connection))
                    {
                        command.Parameters.AddWithValue("@path", newSchedule.Path);

                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                IDataRecord row = (IDataRecord)reader;
                                newId = (int)row[0];
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
            }
            return newId;
        }

        // Add a user as a collaborator of a schedule.
        public int InsertCollaborator(int scheduleId, string userHash, bool canWrite, bool isOwner)
        {
            if (this.dbConnectionString == null)
            {
                throw new NullReferenceException();
            }
            string collaboratorQuery = "INSERT INTO Collaborators " +
                                       "(schedule, collaborator, canWrite, isOwner) " +
                                       "VALUES (@schedule, @collaborator, @canWrite, @isOwner);";
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(collaboratorQuery, connection))
                {
                    command.Parameters.AddWithValue("@schedule", scheduleId);
                    command.Parameters.AddWithValue("@collaborator", userHash);
                    command.Parameters.AddWithValue("@canWrite", canWrite);
                    command.Parameters.AddWithValue("@isOwner", isOwner);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
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
            return rowsAffected;
        }

        // Delete Schedule
        public int DeleteSchedule(int scheduleId)
        {
            int collaboratorsDeleted = this.DeleteAllCollaborators(scheduleId);
            int rowsAffected = -1;
            if (this.dbConnectionString == null)
            {
                throw new NullReferenceException();
            }
            // set up query
            string query = "DELETE FROM Schedules WHERE Schedules.id=@scheduleId";
            using (SqlConnection connection = new SqlConnection(this.dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@scheduleId", scheduleId);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
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
            return rowsAffected;
        }
        // Remove Collaborators from a Schedule. Called during ScheduleManager.DeleteSchedule().
        private int DeleteAllCollaborators(int scheduleId)
        {
            int rowsAffected = -1;
            if (this.dbConnectionString == null)
            {
                throw new NullReferenceException();
            }
            // set up query
            string query = "DELETE FROM Collaborators WHERE Collaborators.schedule=@scheduleId";
            using (SqlConnection connection = new SqlConnection(this.dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@scheduleId", scheduleId);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
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
            return rowsAffected;
        }

        // Select a Schedule without loading its ScheduleItems.
        public Schedule? SelectScheduleWithoutItems(int scheduleId)
        {
            if (this.dbConnectionString == null)
            {
                throw new NullReferenceException();
            }
            Schedule? result = null;
            // set up query
            string query = "SELECT * FROM Schedules WHERE Schedules.id=@scheduleId";
            using (SqlConnection connection = new SqlConnection(this.dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@scheduleId", scheduleId);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // Unpack query results. This loop should have at most one iteration
                        // due to the primary key uniqueness constraint on the id attribute.
                        // If there were no query results, then this loop will have zero
                        // iterations and the result will be null.
                        while (reader.Read())
                        {
                            IDataRecord row = reader;
                            int? id = (int?)row[0];
                            string? title = (string?)row[1];
                            DateTime? created = (DateTime?)row[2];
                            DateTime? modified = (DateTime?)row[3];
                            string? path = (string?)row[4];

                            // If the results aren't null, then the Schedule can be constructed
                            if (id != null && !string.IsNullOrEmpty(title) &&
                                created != null && modified != null &&
                                !string.IsNullOrEmpty(path))
                            {
                                result = new Schedule(
                                    (int)id,
                                    -1,
                                    (DateTime)created,
                                    (DateTime)modified,
                                    (string)title,
                                    (string)path
                                );
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
            return result;
        }
        // Select a Schedule and load its ScheduleItems.
        // Has the same logic as ScheduleManager.SelectScheduleWithoutItems(),
        // with the additional step for loading ScheduleItems.
        public Schedule? SelectScheduleWithItems(int scheduleId)
        {
            if (this.BaseFilePath == null)
            {
                throw new NullReferenceException();
            }
            if (this.dbConnectionString == null)
            {
                throw new NullReferenceException();
            }
            Schedule? result = null;
            result = this.SelectScheduleWithoutItems(scheduleId);

            // Check that result isn't null before continuing. If
            // it is null, then this block gets skipped and null is
            // returned since there's not going to be a file path to
            // check.
            if (result != null)
            {
                //// Load ScheduleItems for the given schedule
                //ScheduleFileAccessor accessor = new ScheduleFileAccessor();

                // Set up the file path
                string path = this.BaseFilePath + result.Path;
                List<ScheduleItem> items = _fileAccessor.ReadScheduleItems(result.Path);
                foreach (ScheduleItem item in items)
                {
                    result.AddScheduleItem(item);
                }
            }
            return result;
        }

        // Creates a ScheduleItem and adds it to a given Schedule
        public bool CreateScheduleItem(ref Schedule s, ref ScheduleItem si)
        {
            bool created = false;
            try
            {
                // If there are items in the Schedule, assign si an ID
                // equal to its index in the list s.Items
                // First, add si to s
                s.AddScheduleItem(si);

                // Then, generate its ID based on its index
                int id = s.Items.Count - 1;

                // Finally, assign its ID
                s.Items[id].Id = id;
                created = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception has occurred: " +
                                ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
            return created;
        }
        // Updates a ScheduleItem si for a given Schedule s
        public bool UpdateScheduleItem(ref Schedule s, ref ScheduleItem si)
        {
            int i = 0;
            bool updated = false;

            while (i < s.Items.Count || !updated)
            {
                // Check that the ID of the ScheduleItems match. Since we don't
                // know what fields in si have changed, we can only rely on the ID
                // being the same.
                if (s.Items[i].Id == si.Id)
                {
                    try
                    {
                        s.Items[i] = si;
                        updated = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("The following exception has occurred: " +
                                ex.GetType().FullName);
                        Console.WriteLine(ex.Message);
                    }
                }
                i++;
            }
            return updated;
        }
        // Deletes a ScheduleItem si from a given Schedule s
        public bool DeleteScheduleItem(ref Schedule s, ScheduleItem si)
        {
            int i = 0;
            bool deleted = false;
            while (i <= s.Items.Count || !deleted)
            {
                if (s.Items[i] == si)
                {
                    try
                    {
                        s.Items.RemoveAt(i);
                        deleted = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("The following exception has occurred: " +
                                ex.GetType().FullName);
                        Console.WriteLine(ex.Message);
                    }
                }
                i++;
            }
            return deleted;
        }
    }
}
