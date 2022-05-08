using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.StudentDiscounts;
using StudentMultiTool.Backend.Services.StudentDiscounts;
using StudentMultiTool.Backend.Services.UserManagement;

namespace StudentMultiTool.Backend.Controllers
{
    [Route("api/" + "studentdiscounts")]
    [ApiController]
    public class StudentDiscountsController : ControllerBase
    {
        private readonly ILogger<StudentDiscounts> _logger;
        public StudentDiscountsController(ILogger<StudentDiscounts> logger)
        {
            _logger = logger;
        }

        // post student discount for Establishment
        [HttpPost("postEstablishment")]
        public IActionResult postEstablishment(StudentDiscounts stDiscount)
        {
            DiscountsManager discount = new DiscountsManager();
            if(discount.postDiscountEstablishment(stDiscount.Title, stDiscount.Name, stDiscount.Address,
                stDiscount.Latitud, stDiscount.Longitud, stDiscount.Description))
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest();
            }

        }

        // post student discount for Website
        [HttpPost("postWebsite")]
        public IActionResult postWebsite(StudentDiscounts stDiscount)
        {
            DiscountsManager discount = new DiscountsManager();
            if (discount.postDiscountWebsite(stDiscount.Title, stDiscount.Website, stDiscount.Description))
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest();
            }

        }

        // get discounts for Websites
        [HttpGet("getDiscountsWebsite")]
        public IEnumerable<DiscountsWeb> getDiscountsWebsite()
        {

            DiscountsManager discount = new DiscountsManager();
            return discount.getDiscountsWeb();

        }

        // get discount details for website
        [HttpGet("getWebDetails/{id}")]
        public IEnumerable<DiscountsWeb> getDetailsWebsite(string id)
        {

            DiscountsManager discount = new DiscountsManager();
            return discount.getWebDetails(id);

        }

        // get discounts for Establishments
        [HttpGet("getDiscountsEstablishment")]
        public IEnumerable<DiscountsEstabl> getDiscountsWe()
        {

            DiscountsManager discount = new DiscountsManager();
            return discount.getDiscountsEstablishment();

        }

        // get discount details for establishment
        [HttpGet("getEstDetails/{id}")]
        public IEnumerable<DiscountsEstabl> getDetailsEstablishment(string id)
        {

            DiscountsManager discount = new DiscountsManager();
            return discount.getEstDetails(id);

        }
    }
}
