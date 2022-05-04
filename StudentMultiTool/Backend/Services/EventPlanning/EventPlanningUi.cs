using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Services.EventPlanning
{
    public class EventPlanningUi
    {
        public EventPlanningUi() { }

        public IEnumerable<EventPlanning> getDetails(string attribute)
        {
            string eventQuery = "SELECT * FROM EventPosts WHERE id = @id";
            List<EventPlanning> result = new List<EventPlanning>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand(eventQuery, conn);
            cmd.Parameters.AddWithValue("@id", attribute);
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string eventtitle = (string)reader["eventtitle"];
                        string eventtime = (string)reader["eventtime"];
                        string date = (string)reader["date"];
                        string location = (string)reader["location"];
                        string description = (string)reader["description"];
                        EventPlanning post = new EventPlanning(id, eventtitle, eventtime, date, location, description);
                        result.Add(post);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }
        public IEnumerable<EventPlanning> getEventPlanning()
        {
            string eventQuery = "SELECT * FROM EventPosts WHERE type = @type";
            List<EventPlanning> result = new List<EventPlanning>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand(eventQuery, conn);
            cmd.Parameters.AddWithValue("@type", "Establishment");
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string eventtitle = (string)reader["eventtitle"];
                        string eventtime = (string)reader["eventtime"];
                        string date = (string)reader["date"];
                        string location = (string)reader["location"];
                        string description = (string)reader["description"];
                        EventPlanning post = new EventPlanning(id, eventtitle, eventtime, date, location, description);
                        result.Add(post);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }
    
        public bool postEevent(string eventtitle, string eventtime, string date, string loaction, string description)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO EventPosts " + "(eventtitle, eventtime, date, loaction, description) " +
                                                                   "  VALUES (@type, @eventtitle, @eventtime, @date, @loaction, @description)", conn);
                cmd.Parameters.AddWithValue("@eventtitle", eventtitle);
                cmd.Parameters.AddWithValue("@eventtime", eventtime);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@location", loaction);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool updateEevent(int id, string eventtitle, string eventtime, string date, string loaction, string description)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE EventPosts " + "SET eventtitle=@eventtitle, eventtime=@eventtime, date=@date, loaction=@loaction, description=@description where id=@id)", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@eventtitle", eventtitle);
                cmd.Parameters.AddWithValue("@eventtime", eventtime);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@location", loaction);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool deleteEevent(int id)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Schedules WHERE Schedules.id = @scheduleId", conn);
                cmd.Parameters.AddWithValue("@id", id);
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
