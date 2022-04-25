using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Services.StudentDiscounts
{
    public class DiscountsManager
    {
        public DiscountsManager() { }

        public IEnumerable<DiscountsWeb> getWebDetails(string attribute)
        {
            string discountsQuery = "SELECT * FROM Discounts WHERE id = @id";
            List<DiscountsWeb> result = new List<DiscountsWeb>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand(discountsQuery, conn);
            cmd.Parameters.AddWithValue("@id", attribute);
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string title = (string)reader["title"];
                        string webside = (string)reader["website"];
                        string description = (string)reader["description"];
                        string dataCreated = (string)reader["dateCreated"];
                        int likes = (int)reader["likes"];
                        int dislikes = (int)reader["dislikes"];
                        DiscountsWeb discount = new DiscountsWeb(id, title, webside, description, dataCreated, likes, dislikes);
                        result.Add(discount);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }

        public IEnumerable<DiscountsWeb> getDiscountsWeb()
        {
            string discountsQuery = "SELECT * FROM Discounts WHERE type = @type";
            List<DiscountsWeb> result = new List<DiscountsWeb>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand(discountsQuery, conn);
            cmd.Parameters.AddWithValue("@type", "Website");
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string title = (string)reader["title"];
                        string webside = (string)reader["website"];
                        string description = (string)reader["description"];
                        string dataCreated = (string)reader["dateCreated"];
                        int likes = (int)reader["likes"];
                        int dislikes = (int)reader["dislikes"];
                        DiscountsWeb discount = new DiscountsWeb(id, title, webside, description, dataCreated, likes, dislikes);
                        result.Add(discount);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }

        public bool postDiscountEstablishment(string name, string title, string address, string latitud, string longitude, string description)
        {
            try
            {
                DateTime dateTime = DateTime.Today;
                string dateCreated = dateTime.ToString("MM/dd/yyy");
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Discounts " + "(type, name, title, address, latitud, longitud, description, dateCreated) " +
                                                                   "  VALUES (@type, @name, @title, @address, @latitud, @longitud, @description, @dateCreated)", conn);
                cmd.Parameters.AddWithValue("@type", "Establishment");
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@latitud", latitud);
                cmd.Parameters.AddWithValue("@longitud", longitude);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@dateCreated", dateCreated);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool postDiscountWebsite(string title, string website, string description)
        {
            try
            {
                DateTime dateTime = DateTime.Today;
                string dateCreated = dateTime.ToString("MM/dd/yyyy");
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Discounts " + "(type, title, website, description, dateCreated) " +
                                                                   "  VALUES (@type, @title, @website, @description, @dateCreated)", conn);
                cmd.Parameters.AddWithValue("@type", "Website");
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@website", website);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@dateCreated", dateCreated);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
        }
            catch (Exception ex)
            {
                return false;
            }
}
    }
}
