
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Services.DataAccess
{
    public class SQLServerDAO : DataSource<string>
    {
        private SqlConnection _conn;

        public SQLServerDAO()
        {
            try
            {
                //Console.WriteLine("Establising Connection...");
                _conn = new SqlConnection(Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING", EnvironmentVariableTarget.User));
                //Console.WriteLine("Connection established.");
            }
            catch
            {
                //_conn = null;
                Console.WriteLine("Error when creating a connection");
                throw;
            }
        }

        // Getter and setter for Odbc
        public SqlConnection Connection { get; set; }

        public SqlConnection GetConnection()
        {
            return _conn;
        }

        // TODO: Closing a connection here will cause a problem
        // Make sure whoever called this need to close the connection until we can fixed it. 
        public Object? ReadData(string cmd)
        {
            try
            {
                _conn.Open();
                SqlCommand command = new SqlCommand(cmd, _conn);
                SqlDataReader reader = command.ExecuteReader();

                //_conn.Close();
                return reader;

            }
            catch (Exception e)
            {
                Console.WriteLine("Error when Reading data from databse.");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        // The also Identical to update data, maybe only one method is enough
        public bool DeleteData(string cmd)
        {
            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand(cmd, _conn);
                command.ExecuteNonQuery();

                _conn.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when deleting data from database!!");
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _conn.Close();
            }
        }


        // TODO: No idea how to check if the command is valid or where to check it
        public bool UpdateData(string cmd)
        {
            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand(cmd, _conn);
                command.ExecuteNonQuery();

                _conn.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when updating data from database!!");
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _conn.Close();
            }
        }

        // Very similar to UpdataData if not identical
        public bool WriteData(string cmd)
        {
            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand(cmd, _conn);
                command.ExecuteNonQuery();

                _conn.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when Writing data from database!!");
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
