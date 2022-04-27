using System.Data;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Models.RecipeModel
{
    public class RecipeDB
    {  
        string connection = @"Server=(localdb)\MSSQLLocalDB;Database=RecipeData;Trusted_Connection=True; MultipleActiveResultSets=true;";


        public List<Recipe> GetAllRecipe()
        {
            List<Recipe> customerRecipe = new List<Recipe>();

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

                //con.Close();

            }
            return customerRecipe;
        }


        public Recipe GetSingleRecipe(int id)
        {
            Recipe oneRecipe = new Recipe ();

            using (SqlConnection con = new SqlConnection(connection))
            {

                SqlCommand cmd = new SqlCommand("SELECT * FROM [recipes]where id ="+ id +";", con);

                //SqlCommand cmd = new SqlCommand("SELECT * FROM [recipes] where recipes.id=@recipeID;", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                try
                {
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        oneRecipe.id = Convert.ToInt32(rd["id"]);
                        oneRecipe.title = rd["title"].ToString();
                        oneRecipe.category = rd["category"].ToString();
                        oneRecipe.calorieValue = rd["calorieValue"].ToString();
                        oneRecipe.overallPrice = rd["overallPrice"].ToString();
                        oneRecipe.datePosted = rd["datePosted"].ToString();
                        oneRecipe.mealForPeople = Convert.ToInt32(rd["mealForPeople"]);
                        oneRecipe.description = rd["description"].ToString();
                   

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }


                con.Close();

            }
            return oneRecipe;
        }
        public bool addValue(Recipe r)
        {
            bool result;
            using (SqlConnection con = new SqlConnection(connection))
            {
                String query = "INSERT INTO [recipes] (title, category, calorieValue, overallPrice, datePosted, mealForPeople, description) VALUES (@ti, @ca, @cal, @price, @date, @meal, @desc);";
                

                SqlCommand cmd = new SqlCommand(query, con);
                //cmd.Parameters.AddWithValue("@id", r.id);//131);
                cmd.Parameters.AddWithValue("@ti", r.title);// "Noodles");
                cmd.Parameters.AddWithValue("@ca", r.category);//"launch");
                cmd.Parameters.AddWithValue("@cal", r.calorieValue);//"80 calories");
                cmd.Parameters.AddWithValue("@price", r.overallPrice);//"$8.0");
                cmd.Parameters.AddWithValue("@date", r.datePosted);//"4/23/2022");
                cmd.Parameters.AddWithValue("@meal", r.mealForPeople);//"2");
                cmd.Parameters.AddWithValue("@desc", r.description);//"Yummmay delicious");

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    result = true;
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    result = false;
                }
            }

            return result;

        }

        public bool updateValue(Recipe r)
        {
            bool result;

            using (SqlConnection con = new SqlConnection(connection))
            {
                String query = "UPDATE [recipes] SET id = @id, title = @ti, category = @ca, calorieValue = @cal, overallPrice= @price, datePosted = @date, mealForPeople = @meal, description = @desc where id = @id;";

                SqlCommand cmd = new SqlCommand(query, con);


                try
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", r.id);//131);
                    cmd.Parameters.AddWithValue("@ti", r.title);// "Noodles");
                    cmd.Parameters.AddWithValue("@ca", r.category);//"launch");
                    cmd.Parameters.AddWithValue("@cal", r.calorieValue);//"80 calories");
                    cmd.Parameters.AddWithValue("@price", r.overallPrice);//"$8.0");
                    cmd.Parameters.AddWithValue("@date", r.datePosted);//"4/23/2022");
                    cmd.Parameters.AddWithValue("@meal", r.mealForPeople);//"2");
                    cmd.Parameters.AddWithValue("@desc", r.description);//"Yummmay delicious");
                    cmd.ExecuteNonQuery();
                    result = true;
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    result = false;
                }

            }

            return result;

        }

        public bool deleteValue(int id)
        {
            bool result;
            using (SqlConnection con = new SqlConnection(connection))
            {
                String query = "DELETE FROM [recipes] where id = @id;";


                SqlCommand cmd = new SqlCommand(query, con);


                try
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    result = true;
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    result = false;
                }

            }
            return result;
        }
    }
}
