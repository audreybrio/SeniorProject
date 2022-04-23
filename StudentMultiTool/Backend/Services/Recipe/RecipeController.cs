using StudentMultiTool.Backend.Models.RecipeModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;

namespace StudentMultiTool.Backend.Services.Recipe
{
    [ApiController]
    [Route("api/" + "recipes")]
    public class RecipeController: Controller
    {
        RecipeDB db = new RecipeDB();

        [HttpGet("getlist")]
        public IActionResult GetSchedule()
        {
            ViewBag.Message = db.GetAllRecipe();

            return View(ViewBag.Message);
        }

    }
}
