using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using StudentMultiTool.Backend.Models.CareerOpportunities;
using StudentMultiTool.Backend.Services.CareerOpportunities;
using static StudentMultiTool.Backend.Services.CareerOpportunities.CareerManager;

namespace StudentMultiTool.Backend.Controllers
{
    [Route("api/" + "careerOpportunities")]
    [ApiController]
    public class CareerOpportunitiesController : ControllerBase
    {
        private readonly ILogger<Opportunities> _logger;
        public CareerOpportunitiesController(ILogger<Opportunities> logger)
        {
            _logger = logger;
        }

        // It creates a Http GET request to USAJobs with a keyword string
        // and returns a Opportunities object
        [HttpGet("getOpportunities/{keywords}")]
        public async Task<Opportunities> getOpportunities(string keywords)
        {
            
            CareerManager careerManager = new CareerManager();

            return await careerManager.getResults(keywords);
        }

    }
}

