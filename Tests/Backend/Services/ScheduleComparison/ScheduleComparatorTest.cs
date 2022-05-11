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
    public class ScheduleComparatorTest
    {
        [Fact]
        public void DurationEqualsZero()
        {
            TimeOnly start = new TimeOnly(1, 0);
            TimeOnly end = new TimeOnly(1, 0);
            ScheduleComparator scheduleComparator = new ScheduleComparator();
            int result = scheduleComparator.Duration(start, end);
            Assert.Equal(0, result);
        }
        [Fact]
        public void DurationMinutes()
        {
            TimeOnly start = new TimeOnly(1, 0);
            TimeOnly end = new TimeOnly(1, 1);
            ScheduleComparator scheduleComparator = new ScheduleComparator();
            int result = scheduleComparator.Duration(start, end);
            Assert.Equal(1, result);
        }
        [Fact]
        public void DurationHours()
        {
            TimeOnly start = new TimeOnly(1, 0);
            TimeOnly end = new TimeOnly(2, 0);
            ScheduleComparator scheduleComparator = new ScheduleComparator();
            int result = scheduleComparator.Duration(start, end);
            Assert.Equal(1, result);
        }

        [Fact]
        public void OverlappingMinutesEqualsZero()
        {
            TimeOnly start = new TimeOnly(1, 0);
            TimeOnly end = new TimeOnly(2, 0);

            ScheduleItem a = new ScheduleItem();
            a.StartTime = start;
            a.EndTime = end;

            ScheduleItem b = new ScheduleItem();
            b.StartTime = start;
            b.EndTime = end;

            ScheduleComparator scheduleComparator = new ScheduleComparator();
            int result = scheduleComparator.OverlappingMinutes(a, b);
            Assert.Equal(0, result);
        }

        [Fact]
        public void OverlappingMinutesNonZero()
        {
            ScheduleItem a = new ScheduleItem();
            a.StartTime = new TimeOnly(1, 0);
            a.EndTime = new TimeOnly(2, 0);

            ScheduleItem b = new ScheduleItem();
            b.StartTime = new TimeOnly(1, 30);
            b.EndTime = new TimeOnly(2, 30);

            ScheduleComparator scheduleComparator = new ScheduleComparator();
            int result = scheduleComparator.OverlappingMinutes(a, b);
            Assert.Equal(30, result);
        }
    }
}
