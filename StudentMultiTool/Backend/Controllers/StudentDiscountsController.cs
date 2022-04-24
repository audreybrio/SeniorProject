﻿using Microsoft.AspNetCore.Http;
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
        [HttpGet("postDiscount/{title}/{name}/{address}/{latitude}/{longitud}/{description}")]
        public IActionResult postEstablishment(string title, string name, string address, string latitude, string longitud, string description)
        {
            DiscountsManager discount = new DiscountsManager();
            if(discount.postDiscountEstablishment(title, name, address, latitude, longitud, description))
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest();
            }

        }
    }
}