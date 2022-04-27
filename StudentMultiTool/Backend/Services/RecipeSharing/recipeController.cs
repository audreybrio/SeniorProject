using StudentMultiTool.Backend.Models.RecipeModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;

namespace StudentMultiTool.Backend.Services.RecipeSharing
{
    [Route("api/[controller]")]
    [ApiController]
    public class recipeController: Controller
    {

        RecipeDB _recipeDB = new RecipeDB();

        [HttpGet("getRecipe")] 
        public IActionResult Get()
        {

            return new JsonResult(_recipeDB.GetAllRecipe());
         
        }


        [HttpGet("getRecipe/{id}")]
        public IActionResult Get(int id)
        {

            return new JsonResult(_recipeDB.GetSingleRecipe(id));

        }


        [HttpPost("newRecipe")]
        public IActionResult Post(Recipe r)
        {

            bool result = _recipeDB.addValue(r);
            string m;
            if (result)
            {
                m = "Successfully Added";
            }
            else
            {
                m = "Error";
            }

            return new JsonResult(m);
        }

        [HttpPut("updateRecipe/{id}")]
        public IActionResult Put(Recipe r)
        {

            bool result = _recipeDB.updateValue(r);
            string m;
            if (result)
            {
                m = "Successfully Updated";
            }
            else
            {
                m = "Error";
            }


            return new JsonResult(m);

        }

        [HttpDelete("deleteRecipe/{id}")]
        public IActionResult Delete(int id)
        {
            bool result = _recipeDB.deleteValue(id);
            string m;
            if (result)
            {
                m = "Successfully Deleted";
            }
            else
            {
                m = "Error";
            }
            return new JsonResult(m);


        }


    }
}
