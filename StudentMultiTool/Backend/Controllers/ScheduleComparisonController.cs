using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleComparison;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/schedulecomparison")]
    public class ScheduleComparisonController : Controller
    {
        // Return a "list" (enumerable) of schedules for a given user.
        // Only returns a list of schedules that the user is listed as a
        // collaborator for.
        // Will always return a list, but there's always the possibility
        // that the list will be empty.
        [HttpGet("getlist/{username}")]
        public IEnumerable<Schedule> GetList(string username)
        {
            // TODO: check that the user is authenticated
            // TODO: convert WL's to logs
            Console.WriteLine("ScheduleController.GetList with args");
            Console.WriteLine("\tusername: \"" + username + "\"");
            // Get user hash so we know who to get schedules for
            string? userHash = null;
            Console.WriteLine("Getting user hash for user \"" + username + "\"");
            ScheduleListBuilder builder = new ScheduleListBuilder();
            userHash = builder.GetUserHash(username);
            if (userHash == null)
            {
                Console.WriteLine("userHash was null");
                return Enumerable.Empty<Schedule>();
            }

            // Get all schedules the user owns (and/or can collaborate on)
            IEnumerable<Schedule> list = builder.GetAllSchedulesForUser(userHash).Result;
            Console.WriteLine("Request finished");
            if (list == null)
            {
                return Enumerable.Empty<Schedule>();
            }
            return list;
        }

        // Return a comparison of schedules given the user and the IDs of the schedules to compare.
        // Always returns an enumerable, but the enumerable may be empty.
        [HttpGet("getcomparison")]
        //public IEnumerable<ScheduleItemDTO> GetComparison([FromQuery] string user, [FromQuery] List<string> scheduleIds)
        public IEnumerable<ScheduleItemDTO> GetComparison([FromQuery] string user, [FromQuery] List<string> scheduleIds)
        {
            Console.WriteLine("ScheduleController.GetSchedule with args");
            Console.WriteLine("\tuser: \"" + user + "\"");
            Console.WriteLine("\tscheduleId: \"" + scheduleIds + "\"");
            List<ScheduleItemDTO> items = new List<ScheduleItemDTO>();
            List<int> ids = new List<int>();

            if (scheduleIds.Count > 0)
            {
                List<string> rawIds = JsonSerializer.Deserialize<List<string>>(scheduleIds[0]);
                foreach (string id in rawIds)
                {
                    Console.WriteLine("id: \"" + id + "\"");
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
                ScheduleManager manager = new ScheduleManager();
                List<Schedule> schedules = new List<Schedule>();
                for (int i = 0; i < ids.Count; i++)
                {
                    Schedule? current = manager.SelectScheduleWithItems(ids[i]);
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
                Console.WriteLine("Request finished with " + items.Count + " items");

                // Return the items
                return items;
            }
            // If something went wrong or the user wasn't a collaborator, return an empty list
            return Enumerable.Empty<ScheduleItemDTO>();
        }
    }
}
