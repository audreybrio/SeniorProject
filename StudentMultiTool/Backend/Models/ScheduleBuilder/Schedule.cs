using System.Text.Json.Nodes;

namespace StudentMultiTool.Backend.Models.ScheduleBuilder
{
    // Represents a user's schedule and details about who can access it.
    // Daily and/or weekly events are stored as ScheduleItems.
    // This is not a calendar; each Schedule represents a generic week of
    // reoccurring events, including (but not limited to) college classes,
    // shifts at work, commutes, and other commitments.
    public class Schedule
    {
        // The actual ScheduleItems for this Schedule.
        public List<ScheduleItem> Items { get; set; } = new List<ScheduleItem>();
        
        public int? Id { get; set; } = null;
                
        // These are user IDs for each user listed as a collaborater on the schedule
        public List<int> Collaborators { get; set; } = new List<int>();
        
        // The owner-assigned title of the schedule
        public string Title { get; set; }

        // The user ID of the owner/original creator of the schedule
        public int OwnerId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        
        // The relative file path used for schedule items
        public string Path { get; set; }

        public Schedule(int OwnerId, DateTime Created, DateTime Modified, string Title, string Path)
        {
            this.OwnerId = OwnerId;
            this.Created = Created;
            this.Modified = Modified;
            this.Title = Title;
            this.Path = Path;
        }

        public Schedule(int Id, int OwnerId, DateTime Created, DateTime Modified, string Title, string Path)
        {
            this.Id = Id;
            this.OwnerId = OwnerId;
            this.Created = Created;
            this.Modified = Modified;
            this.Title = Title;
            this.Path = Path;
        }

        // Adds a ScheduleItem to the Schedule.
        public void AddScheduleItem(ScheduleItem newItem)
        {
            try
            {
                Items.Add(newItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Adds a Collaborator to the Schedule
        public void AddCollaborator(int collaboratorId)
        {
            try
            {
                Collaborators.Add(collaboratorId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Removes a Collaborator from the Schedule
        public void RemoveCollaborator(int collaboratorId)
        {
            try
            {
                Collaborators.Remove(collaboratorId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Returns this Schedule as a JsonObject
        public JsonObject ToJson()
        {
            JsonObject result = new JsonObject
            {
                [ScheduleItemOptions.JsonArrayCount] = Items.Count,
            };
            JsonArray scheduleItems = new JsonArray();
            foreach (ScheduleItem si in Items)
            {
                JsonObject currentValue = si.ToJson();
                scheduleItems.Add(currentValue);
            }
            result.Add(ScheduleItemOptions.JsonArrayName, scheduleItems);
            return result;
        }
    }
}
