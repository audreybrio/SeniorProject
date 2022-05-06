using System.Data;
using System.Data.SqlClient;
using StudentMultiTool.Backend.Services.DataAccess;
using StudentMultiTool.Backend.Models.ScheduleBuilder;

namespace StudentMultiTool.Backend.Services.ScheduleBuilder
{
    // Manages CRUD operations for Schedules.
    public class ScheduleManager
    {
        public string? dbConnectionString { get; set; } = null;
        private string _baseFilePath = string.Empty;
        private ScheduleFileAccessor _fileAccessor = new ScheduleFileAccessor(true);
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
                //int lastIndex = value.Length - 1;
                //if (!_baseFilePath[lastIndex].Equals("/"))
                //{
                //    _baseFilePath = value + "/";
                //}
                //else
                //{
                //    _baseFilePath = value;
                //}
            }
        }
        public ScheduleManager()
        {
            dbConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            BaseFilePath = "../smt-storage/schedules/";
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
            SqlCommandRunner runner = new SqlCommandRunner(dbConnectionString);
            runner.Query = scheduleQuery;
            runner.AddParam("@title", newSchedule.Title);
            runner.AddParam("@created", newSchedule.Created);
            runner.AddParam("@modified", newSchedule.Modified);
            runner.AddParam("@path", newSchedule.Path);
            rowsAffected = runner.ExecuteNonQuery();
            //Console.WriteLine("paramsAdded " + paramsAdded);
            //Console.WriteLine("rowsAffected " + rowsAffected);
            if (rowsAffected == 1)
            {
                runner.Query = scheduleIDQuery;
                runner.AddParam("@path", newSchedule.Path);
                List<object[]> queryResults = runner.ExecuteReader();
                if (queryResults.Count > 0)
                {
                    newId = (int) queryResults[0][0];
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
            SqlCommandRunner runner = new SqlCommandRunner(dbConnectionString);
            runner.Query = collaboratorQuery;
            runner.AddParam("@schedule", scheduleId);
            runner.AddParam("@collaborator", userHash);
            runner.AddParam("@canWrite", canWrite);
            runner.AddParam("@isOwner", isOwner);
            rowsAffected = runner.ExecuteNonQuery();
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
            SqlCommandRunner runner = new SqlCommandRunner(dbConnectionString);
            runner.Query = query;
            runner.AddParam("@scheduleId", scheduleId);
            rowsAffected = runner.ExecuteNonQuery();
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
            SqlCommandRunner runner = new SqlCommandRunner(dbConnectionString);
            runner.Query = query;
            runner.AddParam("@scheduleId", scheduleId);
            rowsAffected = runner.ExecuteNonQuery();
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
            SqlCommandRunner runner = new SqlCommandRunner(dbConnectionString);
            runner.Query = query;
            runner.AddParam("@scheduleId", scheduleId);
            object[] firstRow = runner.ExecuteReaderAsync().Result[0];
            try
            {
                // try to unpack the data and set up the new Schedule
                int? id = (int) firstRow[0];
                string? title = (string) firstRow[1];
                DateTime? created = (DateTime) firstRow[2];
                DateTime? modified = (DateTime) firstRow[3];
                string? path = (string) firstRow[4];

                if (id != null &&
                    !string.IsNullOrEmpty(title) &&
                    created != null &&
                    modified != null &&
                    !string.IsNullOrEmpty(path)
                    )
                {
                    result = new Schedule((int)id,
                                          -1,
                                          (DateTime)created,
                                          (DateTime)modified,
                                          (string)title,
                                          (string)path
                                         );
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.GetType().FullName);
                Console.Error.WriteLine(ex.Message);
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
                List<ScheduleItem> items = _fileAccessor.ReadScheduleItems(path);
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
        // Writes a schedule to a file.
        public string SaveSchedule(ref Schedule s)
        {
            string result = string.Empty;
            try
            {
                result = _fileAccessor.WriteScheduleItems(s, BaseFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception has occurred: " +
                                ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                result = ex.Message;
            }
            return result;
        }

        public int UpdateCollaborator(int scheduleId, string userHash, bool canWrite, bool isOwner)
        {
            if (this.dbConnectionString == null)
            {
                throw new NullReferenceException();
            }
            int result = -1;
            SqlCommandRunner runner = new SqlCommandRunner(dbConnectionString);
            runner.Query = "UPDATE Collaborators SET canWrite = @canWrite, isOwner = @isOwner WHERE schedule = @schedule AND collaborator = @collaborator;";
            runner.AddParam("@canWrite", canWrite);
            runner.AddParam("@isOwner", isOwner);
            runner.AddParam("@schedule", scheduleId);
            runner.AddParam("@collaborator", userHash);
            result = runner.ExecuteNonQuery();
            return result;
        }
        public int DeleteCollaborator(int scheduleId, string userHash)
        {
            if (this.dbConnectionString == null)
            {
                throw new NullReferenceException();
            }
            int result = -1;
            SqlCommandRunner runner = new SqlCommandRunner(dbConnectionString);
            runner.Query = "DELETE FROM Collaborators WHERE schedule = @schedule AND collaborator = @collaborator;";
            runner.AddParam("@schedule", scheduleId);
            runner.AddParam("@collaborator", userHash);
            result = runner.ExecuteNonQuery();
            return result;
        }
    }
}
