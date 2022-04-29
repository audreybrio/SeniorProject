using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Services.EventPlanning;
using StudentMultiTool.Backend.Services.UserManagement;

namespace StudentMultiTool.Backend.Controllers
{
    [Route("api/" + "eventplanning")]
    [ApiController]
    public class EventPlanningController : ControllerBase
    {
        private readonly ILogger<EventPlanning> _logger;
        public EventPlanningController(ILogger<EventPlanning> logger)
        {
            _logger = logger;
        }

        // post event for Establishment
        [HttpPost]
        [HttpPost("postEevent/{eventtitle}/{eventtime}/{date}/{location}/{description}")]
        public IActionResult postEevent(string eventtitle, string eventtime, string date, string location, string description)
        {
            EventPlanningUi post = new EventPlanningUi();
            if(post.postEevent(eventtitle, eventtime, date, location, description))
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest();
            }

        }
       

        // get posts for Establishments
        [HttpGet("getEventPlanning")]
        public IEnumerable<EventPlanning> getDiscountsWe()
        {

            EventPlanningUi post = new EventPlanningUi();
            return post.getEventPlanning();

        }

        // get post details for establishment
        [HttpGet("getDetails /{id}")]
        public IEnumerable<EventPlanning> getDetailsEstablishment(string id)
        {

            EventPlanningUi post = new EventPlanningUi();
            return post.getDetails(id);

        }
    }
}
