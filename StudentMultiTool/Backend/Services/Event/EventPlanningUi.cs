using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Services.Event
{
    public class EventPlanningUi
    {
        string connection = @"Server=LAPTOP-E7T232C2\SQLEXPRESS; Database=ASDB;Trusted_Connection=True; MultipleActiveResultSets=true;";
        public EventPlanningUi() { }

        public IEnumerable<EventPlanning> getDetails()
        {
            string eventQuery = "SELECT * FROM EventPosts";
            List<EventPlanning> result = new List<EventPlanning>();
            SqlConnection conn = new SqlConnection(connection);
            //conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand(eventQuery, conn);
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
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
            conn.Close();
            return result;
        }

        // view single event
        public IEnumerable<EventPlanning> getSingleEvent(int id)
        {
            string eventQuery = "SELECT * FROM EventPosts WHERE id =" + id + ";";
            List<EventPlanning> result = new List<EventPlanning>();
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(eventQuery, conn);
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int Id = (int)reader["id"];
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

        //add event
        public bool addValues(EventPlanning ev)
        {
            bool result;
            try
            {
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand("INSERT INTO EventPosts (eventtitle, eventtime, date, location, description) VALUES (@eventtitle, @eventtime, @date, @location, @description);", conn);

                    conn.Open();
                    cmd.Parameters.AddWithValue("@eventtitle", ev.eventTitle);
                    cmd.Parameters.AddWithValue("@eventtime", ev.eventTime);
                    cmd.Parameters.AddWithValue("@date", ev.Date);
                    cmd.Parameters.AddWithValue("@location", ev.Location);
                    cmd.Parameters.AddWithValue("@description", ev.Description);
                    cmd.ExecuteNonQuery();
                    result = true;

                    conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                result =  false;
            }
               
            
            return result;

        }

        //update event
        public bool updateEevent(int id, EventPlanning ev)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connection);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE EventPosts SET eventtitle=@eventtitle, eventtime=@eventtime, date=@date, location=@location, description=@description where id= " + id + ";", conn);
                //cmd.Parameters.AddWithValue("@id", ev.Id);
                cmd.Parameters.AddWithValue("@eventtitle", ev.eventTitle);
                cmd.Parameters.AddWithValue("@eventtime", ev.eventTime);
                cmd.Parameters.AddWithValue("@date", ev.Date);
                cmd.Parameters.AddWithValue("@location", ev.Location);
                cmd.Parameters.AddWithValue("@description", ev.Description);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //delete event
        public bool deleteEevent(int id)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connection);
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM EventPosts where id= "+ id + ";", conn);
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
