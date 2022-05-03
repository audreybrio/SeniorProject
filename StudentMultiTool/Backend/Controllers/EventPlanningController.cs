using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Services.Event;
using StudentMultiTool.Backend.Services.UserManagement;

namespace StudentMultiTool.Backend.Controllers
{
    [Route("api/" + "eventplanning")]
    [ApiController]
    public class EventPlanningController : ControllerBase
    {

        EventPlanningUi post = new EventPlanningUi();

        private readonly ILogger<EventPlanning> _logger;
        public EventPlanningController(ILogger<EventPlanning> logger)
        {
            _logger = logger;
        }

        [HttpGet("getDetails")]
        public IEnumerable<EventPlanning> getDetailsEstablishment()
        {

            return post.getDetails();

        }

        // view single event
        [HttpGet("getEventPlanning/{id}")]
        public IEnumerable<EventPlanning> getSingle(int id)
        {

            return post.getSingleEvent(id);

        }

        // post all event 
        [HttpPost("postEvent")]
        public IActionResult addValues(EventPlanning e)
        {
            string m;
            if(post.addValues(e))
            {
                m = "Succefully Added";
            }
            else
            {
                m = "error";
            }

            return Ok(m);       
        }

        //update event
        [HttpPut("update/{id}")]
        public IActionResult edit (int id, EventPlanning e)
        {
            string m;
            if (post.updateEevent(id, e))
            {
                m = "Succefully Updated";
            }
            else
            {
                m = "error";
            }

            return Ok(m);


        }

        //delete event
        [HttpDelete("delete/{id}")]
        public IActionResult remove(int id)
        {
            string m;
            if (post.deleteEevent(id))
            {
                m = "Succefully Deleted";
            }
            else
            {
                m = "error";
            }

            return Ok(m);

        }


        // get post details for establishment
     
    }
}
