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

        [HttpGet("getOpportunities")]
        public async Task<Opportunities> getOpportunities()
        {
            
            Console.WriteLine("getOpportunities");
            CareerManager careerManager = new CareerManager();

            return await careerManager.getResults();
        }

    }
}

