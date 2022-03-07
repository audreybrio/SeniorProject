using System.Text.Json;
using System.Text.Json.Nodes;

namespace StudentMultiTool.Backend.Models.ScheduleBuilder
{
    // Represents a user's schedule.
    public class ScheduleFileAccessor
    {
        public static string Success { get; } = "Success";
        public bool Indentation { get; set; }
        public ScheduleFileAccessor(bool Indentation = false)
        {
            this.Indentation = Indentation;
        }

        // Writes all ScheduleItems in a given Schedule to a .json file.
        // The "schedule" argument is simply the Schedule whose ScheduleItems
        // are written to the file. The file path is obtained from the Schedule's
        // Path property.
        // The "indented" argument determines whether or not the resulting
        // file should be indented. Indentation should only be used for demonstrative
        // or testing purposes. When deployed, indentation should not be used, to save
        // storage space.
        public String WriteScheduleItems(Schedule schedule)
        {
            try
            {
                String result = "";
                using (FileStream stream = File.Create(schedule.Path))
                {
                    // Configure writer to indent the .json file, or not.
                    JsonWriterOptions options = new JsonWriterOptions();
                    options.Indented = this.Indentation;

                    using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, options))
                    {
                        // Convert the schedule to a JsonObject
                        // Each ScheduleItem will be automatically converted
                        // to a JsonObject as well, and added to a JsonArray
                        // If there are no ScheduleItems, an empty JsonArray
                        // will be written to the file.
                        JsonObject scheduleAsJson = schedule.ToJson();

                        try
                        {
                            scheduleAsJson.WriteTo(writer);
                            result = ScheduleFileAccessor.Success;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            result = ex.Message;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        // Reads all ScheduleItems in a given .json file.
        public List<ScheduleItem> ReadScheduleItems(String path)
        {
            // Set up the List to store the results
            List<ScheduleItem> result = new List<ScheduleItem>();

            // Check that the file at the specified path actually exists
            // If it doesn't, just return an empty list
            if (!File.Exists(path))
            {
                return result;
            }

            try
            {
                // Get the contents of the file as a string
                String contents = File.ReadAllText(path);

                // Parse the contents
                JsonNode? jsonContents = JsonNode.Parse(contents)!;

                // If the contents are null, return the empty list
                if (jsonContents == null)
                {
                    return result;
                }
                
                // This will be used to update each ScheduleItem's Id property
                int count = 0;

                // If the contents weren't null, try to get the scheduleItems element
                // from the json contents
                // This needs to be cast as an array to read all of the ScheduleItems
                // from it
                JsonArray itemsArray = (JsonArray) jsonContents!["scheduleItems"]!;
                foreach(JsonNode? currentNode in itemsArray)
                {
                    // The currentNode can only be read if it isn't null
                    // If it is null, it will just be skipped
                    if (currentNode != null)
                    {
                        // Instantiate a new ScheduleItem with Creator == 0
                        ScheduleItem currentItem = new ScheduleItem(count, 0);

                        // Update the Creator property
                        currentItem.Creator = (int) currentNode!["creator"]!;

                        // Set the Title, Location, Contact, and Notes
                        currentItem.Title = (String) currentNode!["title"]!;
                        currentItem.Location = (String) currentNode!["location"]!;
                        currentItem.Contact = (String) currentNode!["contact"]!;
                        currentItem.Notes = (String) currentNode!["notes"]!;

                        // Set the days of the week
                        currentItem.DaysOfWeek = new List<bool>
                        {
                            (bool) currentNode!["days"]!["sun"]!,
                            (bool) currentNode!["days"]!["mon"]!,
                            (bool) currentNode!["days"]!["tue"]!,
                            (bool) currentNode!["days"]!["wed"]!,
                            (bool) currentNode!["days"]!["thu"]!,
                            (bool) currentNode!["days"]!["fri"]!,
                            (bool) currentNode!["days"]!["sat"]!
                        };

                        // Set the start and end times
                        currentItem.StartTime = new TimeOnly
                        (
                            (int) currentNode!["start"]!["hour"]!,
                            (int) currentNode!["start"]!["minute"]!
                        );
                        currentItem.EndTime = new TimeOnly
                        (
                            (int) currentNode!["end"]!["hour"]!,
                            (int) currentNode!["end"]!["minute"]!
                        );

                        // Add the unpacked ScheduleItem to the results
                        result.Add(currentItem);

                        // Increment count to use in the next ScheduleItem
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
