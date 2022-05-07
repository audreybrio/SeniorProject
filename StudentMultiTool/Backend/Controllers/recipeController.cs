using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.Recipe;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace StudentMultiTool.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class recipeController : Controller
    {
        private readonly ILogger<recipeController> _logger;

        public recipeController(ILogger<recipeController> logger)
        {
            _logger = logger;
        }

        RecipeDB _recipeDB = new RecipeDB();


        [HttpGet("getlist/_limit={perPage}/_page={page}")]
        public IActionResult Index(int perPage, int page)
        {
            //return new JsonResult (customerRecipe.Skip(skip).Take(perPage).ToList());
            //return new JsonResult(customerRecipe);

            return new JsonResult(_recipeDB.GetAllRecipe(perPage, page));

        }


        [HttpGet("getone/{id}")]
        public IActionResult Get(int id)
        {

            return new JsonResult(_recipeDB.GetSingleRecipe(id));

        }


        [HttpPost("newrecipe")]
        public IActionResult Post(Recipe r)
        {
            string message;
            if (_recipeDB.addValue(r))
            {
                message = "Successfully Added";

            }
            else
            {
                message = "Error";
            }

            return new JsonResult(message);
        }

        [HttpPost("editrecipe/{id}")]
        public IActionResult Put(int id, Recipe r)
        {
            string message;
            if (_recipeDB.updateValue(id, r))
            {
                message = "Successfully Updated";

            }
            else
            {
                message = "Error";
            }



            return new JsonResult(message);

        }


        [HttpPost("deleterecipe/{id}")]
        public IActionResult Delete(int id)
        {
            string message;
            if (_recipeDB.deleteValue(id))
            {
                message = "Successfully Deleted";

            }
            else
            {
                message = "Error";
            }

            return new JsonResult(message);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}