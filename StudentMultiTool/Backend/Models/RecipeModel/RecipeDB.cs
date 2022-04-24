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

        public bool addValue(Recipe r)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                String query = "INSERT INTO [recipes] (id, title, category, calorieValue, overallPrice, datePosted, mealForPeople, description) VALUES (@id, @ti, @ca, @cal, @price, @date, @meal, @desc);";
                

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", r.id);//131);
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
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return true;

        }

        public bool updateValue(Recipe r)
        {

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
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }

            return true;

        }

        public bool deleteValue(int id)
        {

            using (SqlConnection con = new SqlConnection(connection))
            {
                String query = "DELETE FROM [recipes] where id = @id;";


                SqlCommand cmd = new SqlCommand(query, con);


                try
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return true;
        }
    }
}
