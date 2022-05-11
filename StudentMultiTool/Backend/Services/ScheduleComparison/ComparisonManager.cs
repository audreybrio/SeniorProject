using System.Text.Json;
using StudentMultiTool.Backend.DAL;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleBuilder;

namespace StudentMultiTool.Backend.Services.ScheduleComparison
{
    public class ComparisonManager
    {
        // Return a comparison of schedules given the user and the IDs of the schedules to compare.
        // Always returns an enumerable, but the enumerable may be empty.
        public IEnumerable<ScheduleItemDTO> GetComparison(string user, List<string> scheduleIds)
        {
            List<ScheduleItemDTO> items = new List<ScheduleItemDTO>();
            List<int> ids = new List<int>();

            if (scheduleIds != null && scheduleIds.Count > 0)
            {
                // Try to deserialize the numbers from the query string.
                List<string> rawIds;
                try
                {
                    rawIds = JsonSerializer.Deserialize<List<string>>(scheduleIds[0]);
                    foreach (string id in rawIds)
                    {
                        // Try to add the parsed ids into the list to be compared.
                        try
                        {
                            ids.Add(int.Parse(id));
                        }

                        // For all three kinds of exceptions that int.Parse() throws,
                        // we don't want to try to compare anything.
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine(ex.GetType().FullName);
                            Console.Error.WriteLine(ex.Message);
                            return items;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return items;
                }
            }

            // Check that the user has permission to edit the schedule
            SchedulePermissionValidator validator = new SchedulePermissionValidator();
            ScheduleListBuilder listBuilder = new ScheduleListBuilder();
            string? hash = listBuilder.GetUserHash(user);
            bool isCollaborator = false;

            // If the user's hash is null, then they probably aren't logged in
            if (!string.IsNullOrEmpty(hash))
            {
                isCollaborator = validator.ValidateAll(hash, ids);
            }

            // If the user is a collaborator on the schedule, continue
            if (isCollaborator && (ids.Count >= 2 && ids.Count <= 5))
            {
                // Get each schedule's items
                ScheduleDAO dao = new ScheduleDAO();
                List<Schedule> schedules = new List<Schedule>();
                for (int i = 0; i < ids.Count; i++)
                {
                    Schedule? current = dao.SelectScheduleWithItems(ids[i]);
                    if (current != null)
                    {
                        schedules.Add(current);
                    }
                }

                // Compare the schedules
                ScheduleComparator comparator = new ScheduleComparator();
                Schedule result = comparator.VisualRepresentation(schedules);

                // Prep the items for data transfer
                foreach (ScheduleItem si in result.Items)
                {
                    Console.WriteLine(si.Title);
                    ScheduleItemDTO temp = new ScheduleItemDTO(si);
                    temp.ScheduleId = -1;
                    items.Add(temp);
                }

                // Return the items
                return items;
            }
            // If something went wrong or the user wasn't a collaborator, return an empty list
            return Enumerable.Empty<ScheduleItemDTO>();
        }

        // Return a comparison of schedules given the user and the IDs of the schedules to compare.
        // Always returns an integer, but the result may be 0 or -1 in the case of errors.
        public int GetMinutes(string user, List<string> scheduleIds)
        {
            int result = 0;
            List<int> ids = new List<int>();

            if (scheduleIds != null && scheduleIds.Count > 0)
            {
                List<string> rawIds;
                // Try to deserialize the ids from the query string.
                try
                {
                    rawIds = JsonSerializer.Deserialize<List<string>>(scheduleIds[0]);
                    foreach (string id in rawIds)
                    {
                        // Try to add the parsed ids into the list to be compared.
                        try
                        {
                            ids.Add(int.Parse(id));
                        }

                        // For all three kinds of exceptions that int.Parse() throws,
                        // we don't want to try to compare anything.
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine(ex.GetType().FullName);
                            Console.Error.WriteLine(ex.Message);
                            return -1;
                        }
                    }
                }
                // If the query string couldn't be deserialized, we can't really do anything.
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.GetType().FullName);
                    Console.Error.WriteLine(ex.Message);
                    return -1;
                }
            }

            // Check that the user has permission to edit the schedule
            SchedulePermissionValidator validator = new SchedulePermissionValidator();
            ScheduleListBuilder listBuilder = new ScheduleListBuilder();
            string? hash = listBuilder.GetUserHash(user);
            bool isCollaborator = false;

            // If the user's hash is null, then they probably aren't logged in
            if (!string.IsNullOrEmpty(hash))
            {
                isCollaborator = validator.ValidateAll(hash, ids);
            }

            // If the user is a collaborator on the schedule, continue
            if (isCollaborator && (ids.Count >= 2 && ids.Count <= 5))
            {
                // Get each schedule's items
                ScheduleDAO dao = new ScheduleDAO();
                List<Schedule> schedules = new List<Schedule>();
                for (int i = 0; i < ids.Count; i++)
                {
                    Schedule? current = dao.SelectScheduleWithItems(ids[i]);
                    if (current != null)
                    {
                        schedules.Add(current);
                    }
                }

                // Compare the schedules
                ScheduleComparator comparator = new ScheduleComparator();
                result = comparator.TotalFreeMinutes(schedules);

                // Return the total number of shared free minutes
                return result;
            }
            // If something went wrong or the user wasn't a collaborator, return an empty list
            return -1;
        }
    }
}
