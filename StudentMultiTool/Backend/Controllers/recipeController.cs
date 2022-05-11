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

            return new JsonResult(_recipeDB.GetAllRecipe(perPage, page));

        }


        [HttpGet("getone/{id}")]
        public IActionResult Get(int id)
        {
            return new JsonResult(_recipeDB.GetSingleRecipe(id));
        }


        [HttpPost("newrecipe")]
        public IActionResult Post(RecipeList r)
        {
            bool result = false;
            try
            {
                if (_recipeDB.addValue(r))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new JsonResult(result);
        }


        [HttpPost("editrecipe/{id}")]
        public IActionResult Put(int id, RecipeList r)
        {
            bool result = false;

            try
            {
                if (_recipeDB.updateValue(id, r))
                {
                    result = true;

                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return new JsonResult(result);

        }


        [HttpPost("deleterecipe/{id}")]
        public IActionResult Delete(int id)
        {
            bool result = false;

            try
            {
                if (_recipeDB.deleteValue(id))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new JsonResult(result);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}