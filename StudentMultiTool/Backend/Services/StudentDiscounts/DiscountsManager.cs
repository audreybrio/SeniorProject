using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Services.StudentDiscounts
{
    public class DiscountsManager
    {
        public DiscountsManager() { }

        public bool postDiscountEstablishment(string name, string title, string address, string latitud, string longitude, string description)
        {
            try
            {
                DateTime dateTime = DateTime.Today;
                string dateCreated = dateTime.ToString("yyyy/MM/dd");
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

        public bool postDiscountWebsite(string website, string name, string address, string description)
        {
            try
            {
                DateTime dateTime = DateTime.Today;
                string dateCreated = dateTime.ToString("yyyy/MM/dd");
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Discounts " + "(type, name, website, description, dateCreated) " +
                                                                   "  VALUES (@type, @name, @website, @description, @dateCreated)", conn);
                cmd.Parameters.AddWithValue("@type", "Website");
                cmd.Parameters.AddWithValue("@name", name);
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
