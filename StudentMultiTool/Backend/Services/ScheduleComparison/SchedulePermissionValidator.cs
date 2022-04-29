using StudentMultiTool.Backend.Services.DataAccess;
namespace StudentMultiTool.Backend.Services.ScheduleComparison
{
    public class SchedulePermissionValidator
    {
        public string ConnectionString { get; set; } = string.Empty;
        public SchedulePermissionValidator()
        {
            ConnectionString = Environment.GetEnvironmentVariable(EnvironmentVariableEnum.CONNECTIONSTRING)!;
        }
        public SchedulePermissionValidator(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        // Validate a user's permissions for a single schedule by checking
        // If they are listed as a collaborator on it.
        public int IsCollaborator(string user, int scheduleId)
        {
            int result = 0;
            string collaborator = string.Empty;
            bool canWrite = false;
            bool isOwner = false;

            // Configure the query & parameters
            SqlCommandRunner runner = new SqlCommandRunner(ConnectionString);
            runner.Query = "SELECT collaborator, canWrite, isOwner FROM Collaborators WHERE collaborator = @collaborator AND schedule = @scheduleId";
            runner.AddParam("@collaborator", user);
            runner.AddParam("@scheduleId", scheduleId);

            // Get the query results. There should be 1 row returned
            List<object[]> results = runner.ExecuteReader();

            // If there was a row returned, grab the data from it
            if (results.Count > 0)
            {
                try
                {
                    collaborator = (string)results[0][0];
                    canWrite = (bool)results[0][1];
                    isOwner = (bool)results[0][2];
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.GetType().FullName);
                    Console.Error.WriteLine(ex.Message);
                }
            }

            // Check that the collaborator was who they said they were and that they can write to the schedule
            // If there wasn't a row returned, this will always be false due to the right-hand logical statement
            if (collaborator.Equals(user) && (canWrite || isOwner))
            {
                result = 1;
            }
            return result;
        }
        // Validate a user's permissions for multiple schedules. Return true if they are a collaborator on each schedule.
        public bool ValidateAll(string user, List<int> scheduleIds)
        {
            int count = 0;
            int EXPECTED = scheduleIds.Count;
            foreach (int currentId in scheduleIds)
            {
                count += this.IsCollaborator(user, currentId);
            }
            // Check that the number of schedules they are a collaborator on out of the given IDs.
            // The total number of schedules for which they are a collaborator should equal the 
            // number of schedules given.
            return count == EXPECTED;
        }
    }
}
