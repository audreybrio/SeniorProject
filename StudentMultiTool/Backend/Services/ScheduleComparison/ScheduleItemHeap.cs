using StudentMultiTool.Backend.Models.ScheduleBuilder;
namespace StudentMultiTool.Backend.Services.ScheduleComparison
{
    public class ScheduleItemHeap
    {
        private List<ScheduleItem> _list = new List<ScheduleItem>();
        public List<ScheduleItem> List { get { return _list; } }
        public int Size { get { return _list.Count; } }

        public ScheduleItemHeap() {}

        // Add a ScheduleItem to the heap.
        public bool Add(ScheduleItem si)
        {
            _list.Add(si);
            HeapifyBottomUp(Size);
            return true;
        }

        // Re-sort the heap based on the given index.
        public void HeapifyBottomUp(int index)
        {
            int parent = (index / 2) - 1;
            if (index <= 1)
            {
                return;
            }

            // Determine whether or not to swap based on which ScheduleItem comes first.
            // if (_list[index] < list[parent]) swap();
            ScheduleItem? i = null;
            ScheduleItem? p = null;
            try
            {
                i = _list[index];
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.GetType().FullName);
                Console.Error.WriteLine(ex.Message);
            }
            try
            {
                p = _list[parent];
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.GetType().FullName);
                Console.Error.WriteLine(ex.Message);
            }
            
            if (i == null || p == null)
            {
                return;
            }
            bool swap = (i.StartTime < p.StartTime) || (i.StartTime == p.StartTime && i.EndTime < p.EndTime);
            if (swap)
            {
                ScheduleItem temp = _list[index];
                _list[index] = _list[parent];
                _list[parent] = temp;
            }
            HeapifyBottomUp(parent);
        }
    }
}
