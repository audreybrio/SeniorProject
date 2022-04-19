using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleBuilder;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Nodes;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/" + "schedule")]
    public class StudentDiscountsController : Controller
    {
        public ScheduleManager manager { get; } = new ScheduleManager();
    }
}
