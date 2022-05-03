using System;

namespace StudentMultiTool.Backend.Models.Recipe
{
    public class Recipe
    {
        public int id { get; set; }

        public string? title { get; set; }

        public string? category { get; set; }

        public string? calorieValue { get; set; }

        public string? overallPrice { get; set; }

        public string? datePosted { get; set; }

        public int mealForPeople { get; set; }

        public string? description { get; set; }

    }
}
