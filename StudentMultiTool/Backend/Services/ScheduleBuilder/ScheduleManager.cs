using StudentMultiTool.Backend.DAL;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.DataAccess;
using StudentMultiTool.Backend.Services.Logging;
using StudentMultiTool.Backend.Services.ScheduleComparison;
using UserAcc;

namespace StudentMultiTool.Backend.Services.ScheduleBuilder
{
    public class ScheduleManager
    {
        public string Success { get; } = "Success";
        // Return a "list" (enumerable) of schedules for a given user.
        // Only returns a list of schedules that the user is listed as a
        // collaborator for.
        // Will always return a list, but there's always the possibility
        // that the list will be empty.
        public IEnumerable<Schedule> GetList(string username)
        {
            // TODO: check that the user is authenticated
            // Get user hash so we know who to get schedules for
            //string? userHash = null;
            ScheduleListBuilder builder = new ScheduleListBuilder();
            //userHash = builder.GetUserHash(username);
            Hasher hasher = new Hasher();
            string userHash = hasher.HashUsername(username);
            if (userHash == null)
            {
                return Enumerable.Empty<Schedule>();
                //return new List<Schedule>();
            }

            // Get all schedules the user owns (and/or can collaborate on)
            IEnumerable<Schedule> list = builder.GetAllSchedulesForUser(userHash).Result;
            if (list == null)
            {
                return Enumerable.Empty<Schedule>();
                //return new List<Schedule>();
            }
            return list;
        }

        // Return a schedule and its contents based on the ID of the schedule.
        // Always returns an enumerable, but the enumerable may be empty.
        public IEnumerable<ScheduleItemDTO> GetSchedule(string user, int scheduleId)
        {
            List<ScheduleItemDTO> items = new List<ScheduleItemDTO>();

            // Check that the user has permission to edit the schedule
            SchedulePermissionValidator validator = new SchedulePermissionValidator();
            ScheduleListBuilder listBuilder = new ScheduleListBuilder();
            //string? hash = listBuilder.GetUserHash(user);
            Hasher hasher = new Hasher();
            string hash = hasher.HashUsername(user);
            int isCollaborator = 0;

            // If the user's hash is null, then they probably aren't logged in
            if (!string.IsNullOrEmpty(hash))
            {
                isCollaborator = validator.IsCollaborator(hash, scheduleId);
            }

            // If the user is a collaborator on the schedule, continue
            if (isCollaborator > 0)
            {
                // Get the schedule's items
                ScheduleDAO dao = new ScheduleDAO();
                Schedule? schedule = dao.SelectScheduleWithItems(scheduleId);
                if (schedule != null)
                {
                    // Prep the items for data transfer
                    foreach (ScheduleItem si in schedule.Items)
                    {
                        Console.WriteLine(si.Title);
                        ScheduleItemDTO temp = new ScheduleItemDTO(si);
                        temp.ScheduleId = scheduleId;
                        items.Add(temp);
                    }

                    // Return the items
                    return items;
                }
            }
            // If something went wrong or the user wasn't a collaborator, return an empty list
            return Enumerable.Empty<ScheduleItemDTO>();
        }

        // Create a new schedule for a user.
        // Returns the status of the operation.
        public string NewSchedule(string user, string title)
        {
            // TODO: check that the user is authenticated
            int rowsAffected = 0;
            //string? userHash = null;
            Hasher hasher = new Hasher();
            string userHash = hasher.HashUsername(user);

            ScheduleListBuilder builder = new ScheduleListBuilder();
            //userHash = builder.GetUserHash(user);
            if (userHash == null)
            {
                return "Could not get userHash";
            }

            // Add schedule to DB
            Schedule newSchedule = new Schedule(
               -1,
               DateTime.Now,
               DateTime.Now,
               title,
               userHash + "-" + title + ".json"
            );
            ScheduleDAO dao = new ScheduleDAO();
            int? newId = dao.InsertSchedule(newSchedule);

            if (newId != null)
            {
                // Add the owner as a collaborator to the DB
                rowsAffected = dao.InsertCollaborator((int)newId, userHash, true, true);
            }

            if (rowsAffected > 0)
            {
                return Success;
            }

            return "Could not create new schedule";
        }

        public string SaveSchedule(ScheduleDTO data)
        {
            string nullOrEmptyResult = "Request data was null or empty";
            // Make sure the data isn't null or empty
            if (data != null)
            {
                // Get the file path to write the schedule items to
                ScheduleDAO dao = new ScheduleDAO();
                Schedule? schedule = dao.SelectScheduleWithoutItems(data.ScheduleId);
                if (schedule == null)
                {
                    return "Could not find schedule with id " + data.ScheduleId;
                }

                // Unpack the ScheduleItemDTOs and place them in the schedule.
                // The ScheduleItem constructor will handle most of the work here
                foreach (ScheduleItemDTO sid in data.Items)
                {
                    ScheduleItem current = new ScheduleItem(sid);
                    schedule.AddScheduleItem(current);
                }

                // Write the schedule items to the schedule's file
                string result = dao.SaveSchedule(ref schedule);

                // Done!
                if (result.Equals(ScheduleFileAccessor.Success))
                {
                    return Success;
                }
                else
                {
                    return "Could not save schedule to file";
                }

            }
            return nullOrEmptyResult;
        }

        public string AddCollaborator(int scheduleId, string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return "Username cannot be blank";
            }
            ScheduleDAO dao = new ScheduleDAO();
            Schedule? schedule = dao.SelectScheduleWithoutItems(scheduleId);
            if (schedule != null)
            {
                UserAccountDAO uad = new UserAccountDAO();
                UserAccount? user = uad.SelectSingle(username);
                if (user != null)
                {
                    Hasher hasher = new Hasher();
                    string userHash = hasher.HashUsername(username);
                    int rowsAffected = dao.InsertCollaborator(scheduleId, userHash, true, false);
                    if (rowsAffected < 1)
                    {
                        return "Could not add collaborator";
                    }
                    return Success;
                }
                return "User " + username + "could not be found";
            }
            return "Schedule with id " + scheduleId + "could not be found";
        }

        public string UpdateCollaborator(int scheduleId, CollaboratorDTO collaborator)
        {
            if (string.IsNullOrEmpty(collaborator.Username))
            {
                return "Username cannot be blank";
            }
            string username = collaborator.Username;
            ScheduleDAO dao = new ScheduleDAO();
            Schedule? schedule = dao.SelectScheduleWithoutItems(scheduleId);
            if (schedule != null)
            {
                UserAccountDAO uad = new UserAccountDAO();
                UserAccount? user = uad.SelectSingle(username);
                if (user != null)
                {
                    Hasher hasher = new Hasher();
                    string userHash = hasher.HashUsername(username);
                    int rowsAffected = 0;
                    rowsAffected = dao.UpdateCollaborator(scheduleId, userHash, collaborator.CanWrite, collaborator.IsOwner);
                    if (rowsAffected < 1)
                    {
                        return "Could not update collaborator " + username;
                    }
                    return Success;
                }
                return "User " + username + "could not be found";
            }
            return "Schedule with id " + scheduleId + "could not be found";
        }

        public string DeleteCollaborator(int scheduleId, CollaboratorDTO collaborator)
        {
            if (string.IsNullOrEmpty(collaborator.Username))
            {
                return "Username cannot be blank";
            }
            string username = collaborator.Username;
            ScheduleDAO dao = new ScheduleDAO();
            Schedule? schedule = dao.SelectScheduleWithoutItems(scheduleId);
            if (schedule != null)
            {
                UserAccountDAO uad = new UserAccountDAO();
                UserAccount? user = uad.SelectSingle(username);
                if (user != null)
                {
                    Hasher hasher = new Hasher();
                    string userHash = hasher.HashUsername(username);
                    int rowsAffected = 0;
                    rowsAffected = dao.DeleteCollaborator(scheduleId, userHash);
                    if (rowsAffected < 1)
                    {
                        return "Could not delete collaborator " + username;
                    }
                    return Success;
                }
                return "User " + username + "could not be found";
            }
            return "Schedule with id " + scheduleId + "could not be found";
        }
        public IEnumerable<CollaboratorDTO> GetCollaborators(int scheduleId)
        {
            SqlCommandRunner runner = new SqlCommandRunner(Environment.GetEnvironmentVariable(EnvironmentVariableEnum.CONNECTIONSTRING));
            runner.Query = "SELECT username, canWrite, isOwner FROM UserAccounts " +
                "INNER JOIN UserHashes ON UserAccounts.id = UserHashes.id " +
                "INNER JOIN Collaborators ON UserHashes.hash = Collaborators.collaborator " +
                "WHERE Collaborators.schedule = @scheduleId;";
            runner.AddParam("@scheduleId", scheduleId);
            List<object[]> results = runner.ExecuteReader();
            List<CollaboratorDTO> collaborators = new List<CollaboratorDTO>();
            foreach (object[] row in results)
            {
                if (row.Length == 3)
                {
                    collaborators.Add(new CollaboratorDTO(
                        scheduleId,
                        (string) row[0],
                        (bool) row[1],
                        (bool) row[2]
                        ));
                }
            }
            return collaborators;
        }
    }
}
