using Marvel.Services.Logging;
using StudentMultiTool.Backend.DAL;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.Logging;
using StudentMultiTool.Backend.Services.ScheduleBuilder;
using UserAcc;

namespace StudentMultiTool.Backend.Services.UserPrivacy
{
    public class UserPrivacyManager
    {
        private DbLogWriter logger = new DbLogWriter();
        public string Success { get; } = "Success";
        public PrivacyOptions GetOptions(string username)
        {
            PrivacyOptions options = new PrivacyOptions();
            PrivacyOptionsDAO dao = new PrivacyOptionsDAO();
            options.Username = username;
            
            // Set a default value that can be returned in case of error
            options.SellMyInfo = true;

            // Try to get the user's privacy options
            try
            {
                options = dao.SelectPrivacyOptions(username);
            }
            catch (Exception ex)
            {
                _ = logger.AddLog("Error", "Data Access", username, ex.Message);
            }
            return options;
        }

        public string SetOptions(PrivacyOptions options)
        {
            // get the user from the database
            PrivacyOptionsDAO dao = new PrivacyOptionsDAO();
            // update their options
            int result = dao.UpdatePrivacyOptions(options);
            if (result == 1)
            {
                return Success;
            }
            else if (result > 1)
            {
                return "An error occurred in the database, please check the database";
            }
            else
            {
                return "Could not update privacy options for user " + options.Username;
            }
        }

        public string DeleteAccount(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return "username cannot be null";
            }

            // Get the user from the database
            UserAccountDAO uad = new UserAccountDAO();
            UserAccount? user = uad.SelectSingle(username);
            if (user != null)
            {
                // Get the hash of the username; used for some tables
                Hasher hasher = new Hasher();
                string userHash = hasher.HashUsername(username);

                // Delete schedule data
                try
                {
                    ScheduleManager scheduleManager = new ScheduleManager();

                    // Get all schedules that the user 
                    List<Schedule> schedules = (List<Schedule>)scheduleManager.GetList(username);
                    ScheduleDAO scheduleDAO = new ScheduleDAO();

                    // Delete each schedule the user owns
                    foreach (Schedule schedule in schedules)
                    {
                        if (schedule != null)
                        {
                            try
                            {
                                // Remove the user as a collaborator on the schedule
                                int id = (int) schedule.Id;
                                CollaboratorDTO current = new CollaboratorDTO();
                                current.Username = username;
                                current.Schedule = id;
                                scheduleManager.DeleteCollaborator(id, current);

                                // Delete the schedule
                                scheduleDAO.DeleteSchedule(id);
                            }
                            catch (Exception ex)
                            {
                                _ = logger.AddLog("Error", "Data Access", userHash, ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _ = logger.AddLog("Error", "Data Access", userHash, ex.Message);
                }

                // TODO: Delete matching data


                // Delete Privacy options
                PrivacyOptionsDAO pod = new PrivacyOptionsDAO();
                pod.DeletePrivacyOptions(new PrivacyOptions(username));

                // Delete the user
                string result = uad.DeleteSingle(user);
                return result;
            }

            // If the user was null, exit
            return "Could not find user with username \"" + username + "\"";
        }
    }
}
