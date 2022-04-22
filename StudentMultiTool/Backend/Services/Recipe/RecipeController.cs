using StudentMultiTool.Backend.Models.RecipeModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;

namespace StudentMultiTool.Backend.Services.Recipe
{
    [ApiController]
    [Route("api/" + "Recipes")]
    public class RecipeController: Controller
    {
        RecipeDB db = new RecipeDB();

        [HttpGet("getlist/{id}")]
        public IActionResult GetSchedule(int id)
        {
            ViewBag.Message = db.GetAllRecipe(id);

            return View();
        }

    }
}
