using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Services.StudentDiscounts
{
    public class DiscountsManager
    {
        public DiscountsManager() { }

        // getEstDetails returns the details of the Establishments
        public IEnumerable<DiscountsEstabl> getEstDetails(string attribute)
        {
            string discountsQuery = "SELECT * FROM Discounts WHERE id = @id";
            List<DiscountsEstabl> result = new List<DiscountsEstabl>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand(discountsQuery, conn);
            cmd.Parameters.AddWithValue("@id", attribute);
            try
            {
                // It get the values from the table and saves it in a discountsEstablishments class
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string name = (string)reader["name"];
                        string title = (string)reader["title"];
                        string address = (string)reader["address"];
                        string lat = (string)reader["latitud"];
                        string lng = (string)reader["longitud"];
                        string description = (string)reader["description"];
                        DateTime dataCreated = (DateTime)reader["dateCreated"];
                        int likes = (int)reader["likes"];
                        int dislikes = (int)reader["dislikes"];
                        DiscountsEstabl discount = new DiscountsEstabl(id, name, title, address, lat, lng, description, dataCreated.ToString(), likes, dislikes);
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

        // It returns all discounts of the Establishments
        public IEnumerable<DiscountsEstabl> getDiscountsEstablishment()
        {
            string discountsQuery = "SELECT * FROM Discounts WHERE type = @type";
            List<DiscountsEstabl> result = new List<DiscountsEstabl>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand(discountsQuery, conn);
            cmd.Parameters.AddWithValue("@type", "Establishment");
            try
            {
                // It get the values from the table and saves it in a discountsEstablishments class
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string name = (string)reader["name"];
                        string title = (string)reader["title"];
                        string address = (string)reader["address"];
                        string lat = (string)reader["latitud"];
                        string lng = (string)reader["longitud"];
                        string description = (string)reader["description"];
                        DateTime dataCreated = (DateTime)reader["dateCreated"];
                        int likes = (int)reader["likes"];
                        int dislikes = (int)reader["dislikes"];
                        DiscountsEstabl discount = new DiscountsEstabl(id, name, title, address, lat, lng, description, dataCreated.ToString(), likes, dislikes);
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

        // It returns the discounts of web discounts
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
                        DateTime dataCreated = (DateTime)reader["dateCreated"];
                        int likes = (int)reader["likes"];
                        int dislikes = (int)reader["dislikes"];
                        DiscountsWeb discount = new DiscountsWeb(id, title, webside, description, dataCreated.ToString(), likes, dislikes);
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

        // it returns all web discounts
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
                        DateTime dataCreated = (DateTime)reader["dateCreated"];
                        int likes = (int)reader["likes"];
                        int dislikes = (int)reader["dislikes"];
                        DiscountsWeb discount = new DiscountsWeb(id, title, webside, description, dataCreated.ToString(), likes, dislikes);
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

        // It saves the info of a Establishment discount
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

        // It saves the info of a web discount
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
