using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleComparison;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/schedulecomparison")]
    public class ScheduleComparisonController : Controller
    {
        // Return a comparison of schedules given the user and the IDs of the schedules to compare.
        // Always returns an enumerable, but the enumerable may be empty.
        [HttpGet("getcomparison")]
        public IEnumerable<ScheduleItemDTO> GetComparison([FromQuery] string user, [FromQuery] List<string> scheduleIds)
        {
            ComparisonManager manager = new ComparisonManager();
            return manager.GetComparison(user, scheduleIds);
        }

        // Return a comparison of schedules given the user and the IDs of the schedules to compare.
        // Always returns an integer, but the result may be 0 or -1 in the case of errors.
        [HttpGet("getminutes")]
        public int GetMinutes([FromQuery] string user, [FromQuery] List<string> scheduleIds)
        {
            ComparisonManager manager = new ComparisonManager();
            return manager.GetMinutes(user, scheduleIds);
        }
    }
}
