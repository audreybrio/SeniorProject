using StudentMultiTool.Backend.Models.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Backend.Services.Recipe
{
    public class RecipeTest
    {
        [Fact]
        public void RecipeGet()
        {
            int id = 5;

            RecipeDB r = new RecipeDB();

            bool success = r.GetSingleRecipe(id);

            Assert.True(success);

        }

        [Fact]
        public void RecipePost()
        {
            RecipeDB r = new RecipeDB();
            RecipeList recipe = new RecipeList ();
            recipe.title = "Chicken noodles";
            recipe.category = "Dinner";
            recipe.calorieValue = "124 Calories";
            recipe.overallPrice = "$12.45";
            recipe.datePosted = "03/24/2022";
            recipe.mealForPeople = 2;
            recipe.description = " Recipe description goes here.";

            bool success = r.addValue(recipe);

            Assert.False(success);
        }

        [Fact]
        public void RecipeUpdate()
        {
            int id = 6;
            RecipeDB r = new RecipeDB();
            RecipeList recipe = new RecipeList();
            recipe.title = "Chicken noodles";
            recipe.category = "Dinner";
            recipe.calorieValue = "124 Calories";
            recipe.overallPrice = "$12.45";
            recipe.datePosted = "03/24/2022";
            recipe.mealForPeople = 2;
            recipe.description = " Recipe description goes here.";

            bool success = r.updateValue(id, recipe);

            Assert.False(success);

        }

        [Fact]
        public void RecipeDelete()
        {
            int id = 6;
            RecipeDB r = new RecipeDB();

            bool success = r.deleteValue(id);

            Assert.False(success);
        }
    }
}
