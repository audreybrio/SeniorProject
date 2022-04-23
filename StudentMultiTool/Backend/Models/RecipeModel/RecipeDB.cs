using StudentMultiTool.Backend.Services.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Models.RecipeModel
{
    public class RecipeDB
    {

        public List<Recipe> GetAllRecipe()
        {
            List<Recipe> customerRecipe = new List<Recipe>();
            string connection = @"Server=(localdb)\MSSQLLocalDB;Database=RecipeData;Trusted_Connection=True; MultipleActiveResultSets=true;";

            using (SqlConnection con = new SqlConnection(connection))
            {

                SqlCommand cmd = new SqlCommand("SELECT * FROM [recipes];", con);

                //SqlCommand cmd = new SqlCommand("SELECT * FROM [recipes] where recipes.id=@recipeID;", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                try
                {
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {

                        customerRecipe.Add(new Recipe
                        {
                            id = Convert.ToInt32(rd["id"]),
                            title = rd["title"].ToString(),
                            category = rd["category"].ToString(),
                            calorieValue = rd["calorieValue"].ToString(),
                            overallPrice = rd["overallPrice"].ToString(),
                            datePosted = rd["datePosted"].ToString(),
                            mealForPeople = Convert.ToInt32(rd["mealForPeople"]),
                            description = rd["description"].ToString()
                        }); 

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                con.Close();

            }
            return customerRecipe;
        }

    }
}
