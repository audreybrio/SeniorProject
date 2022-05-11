using StudentMultiTool.Backend.Services.ScheduleBuilder;
using StudentMultiTool.Backend.Models.ScheduleBuilder;

namespace StudentMultiTool.Backend.Services.ScheduleComparison
{
    public class ScheduleComparator
    {
        private ScheduleListBuilder schedulelistbuilder { get; } = new ScheduleListBuilder();
        // Return true if and only if a occurs on the given day; otherwise, return false.
        // Return false if day is not a valid option as defined in ScheduleItemOptions.
        public bool OnThisDay(ScheduleItem a, string day)
        {
            if (a == null)
                return false;

            if (day == ScheduleItemOptions.Sunday)
            {
                return a.Sunday;
            }
            else if (day == ScheduleItemOptions.Monday)
            {
                return a.Monday;
            }
            else if (day == ScheduleItemOptions.Tuesday)
            {
                return a.Tuesday;
            }
            else if (day == ScheduleItemOptions.Wednesday)
            {
                return a.Wednesday;
            }
            else if (day == ScheduleItemOptions.Thursday)
            {
                return a.Thursday;
            }
            else if (day == ScheduleItemOptions.Friday)
            {
                return a.Friday;
            }
            else if (day == ScheduleItemOptions.Saturday)
            {
                return a.Saturday;
            }

            // This should handle the case in which the day argument
            // isn't a valid option, for whatever reason
            return false;
        }
        // Return true if and only if a and b occur on the same day; otherwise, return false.
        // Return false if day is not a valid option as defined in ScheduleItemOptions.
        public bool OnSameDay(ScheduleItem a, ScheduleItem b, string day)
        {
            if (a == null || b == null)
                return false;

            if (day == ScheduleItemOptions.Sunday)
            {
                return (a.Sunday && b.Sunday);
            }
            else if (day == ScheduleItemOptions.Monday)
            {
                return (a.Monday && b.Monday);
            }
            else if (day == ScheduleItemOptions.Tuesday)
            {
                return (a.Tuesday && b.Tuesday);
            }
            else if (day == ScheduleItemOptions.Wednesday)
            {
                return (a.Wednesday && b.Wednesday);
            }
            else if (day == ScheduleItemOptions.Thursday)
            {
                return (a.Thursday && b.Thursday);
            }
            else if (day == ScheduleItemOptions.Friday)
            {
                return (a.Friday && b.Friday);
            }
            else if (day == ScheduleItemOptions.Saturday)
            {
                return (a.Saturday && b.Saturday);
            }

            // This should handle the case in which the day argument
            // isn't a valid option, for whatever reason
            return false;
        }

        // Returns -1 if a is earlier than b (a.StartTime < b.StartTime)
        // Returns 0 if a and b are the same (a.StartTime == b.StartTime)
        // Returns 1 if a is later than b    (a.StartTime > b.StartTime)
        public int CompareStartTime(ScheduleItem a, ScheduleItem b)
        {
            // Return -1 if a is earlier than b
            if (a.StartTime < b.StartTime)
            {
                return -1;
            }
            // Returns 0 if a and b are the same
            if (a.StartTime == b.StartTime)
            {
                return 0;
            }
            // Returns 1 if a is later than b
            // (this is logically implicit based on the other two if statements)
            // if (a.StartTime > b.StartTime)
            return 1;
        }

        // Returns -1 if a is earlier than b (a.EndTime < b.EndTime)
        // Returns 0 if a and b are the same (a.EndTime == b.EndTime)
        // Returns 1 if a is later than b    (a.EndTime > b.EndTime)
        public int CompareEndTime(ScheduleItem a, ScheduleItem b)
        {
            // Return -1 if a is earlier than b
            if (a.EndTime < b.EndTime)
            {
                return -1;
            }
            // Returns 0 if a and b are the same
            if (a.EndTime == b.EndTime)
            {
                return 0;
            }
            // Returns 1 if a is later than b
            // (this is logically implicit based on the other two if statements)
            // if (a.EndTime > b.EndTime)
            return 1;
        }
        public int TimeDifference(TimeOnly a, TimeOnly b)
        {
            TimeSpan result = a - b;
            return (int) result.TotalMinutes;
        }
        public int Duration(TimeOnly a, TimeOnly b)
        {
            TimeSpan result = a - b;
            return (int) result.TotalMinutes;
        }
        public int OverlappingMinutes(ScheduleItem a, ScheduleItem b)
        {
            int startOverlap = TimeDifference(a.StartTime, b.StartTime);
            int endOverlap = TimeDifference(a.EndTime, b.EndTime);
            return startOverlap + endOverlap;
        }
        public TimeOnly EarliestTime(TimeOnly a, TimeOnly b)
        {
            // Return a if a < b; else, return b
            return a < b ? a : b;
        }
        public TimeOnly LatestTime(TimeOnly a, TimeOnly b)
        {
            // Return a if a > b; else, return b
            return a > b ? a : b;
        }

        public Schedule VisualRepresentation(List<Schedule> schedules)
        {
            Schedule result = new Schedule(
                0,
                schedules[0].OwnerId,
                DateTime.Now,
                DateTime.Now,
                "Comparison",
                "comparison.json"
                );

            // configure global early & late bounds; used to determine the amount of
            // free time before the earliest ScheduleItem and the latest ScheduleItem
            // on each day
            TimeOnly earliestPossible = new TimeOnly(0, 0);
            TimeOnly latestPossible = new TimeOnly(23, 59);

            // Get the list of days; used to determine which scheduleitems to add to
            // each heap.
            List<string> days = ScheduleItemOptions.Days;

            // Store & sort all items for each day
            for (int i = 0; i < days.Count; i++)
            {
                ScheduleItemHeap currentHeap = new ScheduleItemHeap();
                foreach (Schedule s in schedules)
                {
                    foreach (ScheduleItem si in s.Items)
                    {
                        // add each day's items to its heap
                        if (OnThisDay(si, days[i]))
                        {
                            currentHeap.Add(si);
                        }
                    }
                }

                // Compare the start and end times of each item in the heap
                TimeOnly earlyBound = earliestPossible;
                TimeOnly lateBound = latestPossible;

                // If the current heap is empty, then all schedules are free all day (for this day).
                if (currentHeap.List.Count == 0)
                {
                    ScheduleItem freeAllDay = new ScheduleItem(result.Items.Count + 1);
                    freeAllDay.Title = "Free Time " + freeAllDay.Id.ToString();
                    freeAllDay.StartTime = earliestPossible;
                    freeAllDay.EndTime = latestPossible;
                    freeAllDay.DaysOfWeek[i] = true;
                    result.AddScheduleItem(freeAllDay);
                }

                // Otherwise
                foreach (ScheduleItem si in currentHeap.List)
                {
                    lateBound = si.StartTime;

                    ScheduleItem currentItem = new ScheduleItem(result.Items.Count + 1);
                    currentItem.Title = "Free Time " + currentItem.Id.ToString();
                    currentItem.DaysOfWeek = si.DaysOfWeek;
                    currentItem.StartTime = earlyBound;
                    currentItem.EndTime = lateBound;

                    result.AddScheduleItem(currentItem);
                    earlyBound = si.EndTime;
                }
            }

            return result;
        }

        // Compare two or more schedules to get the free time they share in minutes.
        // Intended to be used for matching.
        public int TotalFreeMinutes(List<Schedule> l)
        {
            int result = 0;
            
            // configure global early & late bounds; used to determine the amount of
            // free time before the earliest ScheduleItem and the latest ScheduleItem
            // on each day
            TimeOnly earliestPossible = new TimeOnly(0, 0);
            TimeOnly latestPossible = new TimeOnly(23, 59);

            // Get the list of days; used to determine which scheduleitems to add to
            // each heap.
            List<string> days = ScheduleItemOptions.Days;

            // Store & Sort all items for each day
            for (int i = 0; i < days.Count; i++)
            {
                ScheduleItemHeap currentHeap = new ScheduleItemHeap();

                // Iterate over each schedule
                foreach (Schedule s in l)
                {
                    // Iterate over each item si in the current schedule s
                    foreach (ScheduleItem si in s.Items)
                    {
                        // check that si doesn't overlap with another ScheduleItem in the heap
                        bool found = false;
                        for (int j = 0; j < currentHeap.Size && !found; j++)
                        {
                            ScheduleItem currentItem = currentHeap.List[j];
                            if (
                                si.DaysOfWeek[i] == currentItem.DaysOfWeek[i] &&
                                 (si.StartTime == currentItem.StartTime ||
                                   si.EndTime == currentItem.EndTime)
                               )
                            {
                                // If an overlap was found, update the item in the heap to have the
                                // earliest of the two start times and the latest of the two end times
                                currentItem.StartTime = EarliestTime(si.StartTime, currentItem.StartTime);
                                currentItem.EndTime = EarliestTime(si.EndTime, currentItem.EndTime);
                                found = true;
                            }
                        }

                        // If si doesn't overlap with any item in the heap, add it to the heap
                        if (!found && OnThisDay(si, days[i]))
                        {
                            currentHeap.Add(si);
                        }
                    }
                }

                // Compare the start and end times of each item in the heap
                TimeOnly earlyBound = earliestPossible;
                TimeOnly lateBound = latestPossible;
                int duration;
                foreach (ScheduleItem si in currentHeap.List)
                {
                    lateBound = si.StartTime;
                    duration = Duration(lateBound, earlyBound);
                    result += duration;
                    earlyBound = si.EndTime;
                }
                duration = Duration(latestPossible, earlyBound);
                result += duration;
            }
            return result;
        }
    }
}
