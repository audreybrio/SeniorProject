using System;
using Xunit;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleComparison;

namespace Tests.Backend.Services.ScheduleComparison
{
    public class ScheduleItemHeapTest
    {
        [Fact]
        public void Add()
        {
            int expected = 16;
            ScheduleItemHeap heap = new ScheduleItemHeap();
            ScheduleItem item = new ScheduleItem();
            for (int i = 0; i < expected; i++)
            {
                heap.Add(item);
            }
            Assert.Equal(expected, heap.Size);
        }
        [Fact]
        public void Size()
        {
            int expected = 16;
            ScheduleItemHeap heap = new ScheduleItemHeap();
            ScheduleItem item = new ScheduleItem();
            for (int i = 0; i < expected; i++)
            {
                heap.Add(item);
            }
            Assert.Equal(expected, heap.Size);
            Assert.Equal(heap.List.Count, heap.Size);
        }
        [Fact]
        public void Sorts()
        {
            int expected = 2;
            ScheduleItemHeap heap = new ScheduleItemHeap();
            ScheduleItem item = new ScheduleItem();
            for (int i = 0; i < expected; i++)
            {
                item.StartTime = new TimeOnly(i, 0);
                heap.Add(item);
            }

            ScheduleItem root = heap.List[0];
            foreach (ScheduleItem si in heap.List)
            {
                if (si != root)
                {
                    Assert.True(
                        (si.StartTime > root.StartTime) ||
                        (si.StartTime == root.StartTime &&
                         si.EndTime < root.EndTime)
                        );
                }
            }
        }
    }
}
