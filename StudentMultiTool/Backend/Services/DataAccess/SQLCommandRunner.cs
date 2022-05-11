using System.Data;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Services.DataAccess
{
    // An object that runs SqlCommand methods and returns results. For Insert, Update, and Delete
    // queries, the return values are based on the success of the query. For Select queries,
    // the return values will be a Dictionary of ints and objects if successful. The user will
    // have to unpack the data themselves. If a Select query is unsuccessful, the Dictionary will
    // be empty.
    // Basic example: 
    //     string myValue = "myValue";
    //     SqlCommandRunner runner = new SqlCommandRunner(connectionString);
    //     runner.Query = "INSERT INTO table VALUES (@value)";
    //     runner.AddParam("@value", myValue);
    //     int rowsAffected = runner.ExecuteNonQuery();
    //     if (rowsAffected == 1)
    //     {
    //         Console.WriteLine("Success!");
    //     }
    //     else
    //     {
    //         Console.WriteLine("Something went wrong");
    //     }
    public class SqlCommandRunner
    {
        public string ConnectionString { get; set; } = string.Empty;
        private string _Query = string.Empty;
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        public string Query
        {
            get
            {
                return _Query;
            }

            set
            {
                _Query = value;
                reset();
            }
        }

        public SqlCommandRunner(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        private void LogError(Exception ex)
        {
            if (ex != null)
            {
                Console.Error.WriteLine(ex.GetType().FullName);
                Console.Error.WriteLine(ex.Message);
            }
        }
        public bool UpdateQuery(string query)
        {
            try
            {
                this._Query = query;
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return false;
        }
        public bool reset()
        {
            try
            {
                Parameters = new Dictionary<string, object>();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }
        // Adds a parameter to the Command
        public int AddParam<T>(string position, T parameter)
        {
            int result = 0;
            try
            {
                Parameters.Add(position, parameter);
                result++;
            }
            catch (ArgumentNullException ex)
            {
                LogError(ex);
            }
            catch (ArgumentException ex)
            {
                LogError(ex);
            }
            return result;
        }

        // Intended for Insert, Update, Delete queries. Returns the number of rows
        // affected by the query, or -1 in case of an error.
        // Don't forget to set the Query and use AddParameters() first!
        public int ExecuteNonQuery()
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(Query))
            {
                return -1;
            }
            int rowsAffected = -1;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    int paramsAdded = 0;
                    foreach (string key in Parameters.Keys)
                    {
                        try
                        {
                            object value = Parameters[key];
                            command.Parameters.AddWithValue(key, value);
                            paramsAdded++;
                        }
                        // I'm not even sure this is possible, but if KeyNotFoundException was to happen
                        // the query would be almost certainly unsuccessful due to the missing parameter
                        // TODO: determine whether or not to catch ArgumentNullExceptions. 
                        catch (KeyNotFoundException ex)
                        {
                            LogError(ex);
                            return rowsAffected;
                        }
                        // Might be better to abort the query in case of some unforseen error.
                        catch (Exception ex)
                        {
                            LogError(ex);
                            return rowsAffected;
                        }
                    }
                    // All parameters should have been added to command. Abort in case of some forseen error.
                    if (paramsAdded != Parameters.Count)
                    {
                        return rowsAffected;
                    }

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (ObjectDisposedException ex)
                    {
                        LogError(ex);
                    }
                    catch (InvalidCastException ex)
                    {
                        LogError(ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        LogError(ex);
                    }
                    catch (IOException ex)
                    {
                        LogError(ex);
                    }
                    catch (SqlException ex)
                    {
                        LogError(ex);
                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (SqlException ex)
                        {
                            LogError(ex);
                        }
                    }
                }
            }
            // reset Parameters, but keep the same query in case the user wants to re-run it
            this.Query = this.Query;
            return rowsAffected;
        }

        // Intended for Insert, Update, Delete queries. Returns the number of rows
        // affected by the query, or -1 in case of an error.
        // Don't forget to set the Query and use AddParameters() first!
        public async Task<int> ExecuteNonQueryAsync()
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(Query))
            {
                return -1;
            }
            int rowsAffected = -1;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    int paramsAdded = 0;
                    foreach (string key in Parameters.Keys)
                    {
                        try
                        {
                            object value = Parameters[key];
                            command.Parameters.AddWithValue(key, value);
                            paramsAdded++;
                        }
                        // I'm not even sure this is possible, but if KeyNotFoundException was to happen
                        // the query would be almost certainly unsuccessful due to the missing parameter
                        // TODO: determine whether or not to catch ArgumentNullExceptions. 
                        catch (KeyNotFoundException ex)
                        {
                            LogError(ex);
                            return rowsAffected;
                        }
                        // Might be better to abort the query in case of some unforseen error.
                        catch (Exception ex)
                        {
                            LogError(ex);
                            return rowsAffected;
                        }
                    }
                    // All parameters should have been added to command. Abort in case of some forseen error.
                    if (paramsAdded != Parameters.Count)
                    {
                        return rowsAffected;
                    }

                    try
                    {
                        connection.Open();
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                    catch (ObjectDisposedException ex)
                    {
                        LogError(ex);
                    }
                    catch (InvalidCastException ex)
                    {
                        LogError(ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        LogError(ex);
                    }
                    catch (IOException ex)
                    {
                        LogError(ex);
                    }
                    catch (SqlException ex)
                    {
                        LogError(ex);
                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (SqlException ex)
                        {
                            LogError(ex);
                        }
                    }
                }
            }
            // reset Parameters, but keep the same query in case the user wants to re-run it
            this.Query = this.Query;
            return rowsAffected;
        }

        // Intended to execute any Select query. We don't know the number of rows that will
        // be returned, so a List allows us dynamically allocated memory.
        // To unpack the results, iterate over the List and grab each result by its index in the
        // row, casting it to its respective type. The number of columns returned in each row
        // should be known before using this.
        // This could return null values!
        // Don't forget to set the Query and use AddParameters() first!
        public List<object[]> ExecuteReader()
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(Query))
            {
                return new List<object[]>();
            }
            List<object[]> results = new List<object[]>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    // Add parameters to the command.
                    int paramsAdded = 0;
                    foreach (string key in Parameters.Keys)
                    {
                        try
                        {
                            object value = Parameters[key];
                            command.Parameters.AddWithValue(key, value);
                            paramsAdded++;
                        }
                        // I'm not even sure this is possible, but if KeyNotFoundException was to happen
                        // the query would be almost certainly unsuccessful due to the missing parameter
                        // TODO: determine whether or not to catch ArgumentNullExceptions. 
                        catch (KeyNotFoundException ex)
                        {
                            LogError(ex);
                            return results;
                        }
                        // Might be better to abort the query in case of some unforseen error.
                        catch (Exception ex)
                        {
                            LogError(ex);
                            return results;
                        }
                    }
                    // All parameters should have been added to command. Abort in case of some forseen error.
                    if (paramsAdded != Parameters.Count)
                    {
                        return results;
                    }

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        int i = 0;
                        while (reader.Read())
                        {
                            IDataRecord currentRow = reader;
                            results.Add(new object[currentRow.FieldCount]);
                            for (int j = 0; j < currentRow.FieldCount; j++)
                            {
                                results[i][j] = currentRow[j];
                            }
                            i++;
                        }
                        reader.Close();
                    }
                    catch (ObjectDisposedException ex)
                    {
                        LogError(ex);
                    }
                    catch (InvalidCastException ex)
                    {
                        LogError(ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        LogError(ex);
                    }
                    catch (IOException ex)
                    {
                        LogError(ex);
                    }
                    catch (SqlException ex)
                    {
                        LogError(ex);
                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (SqlException ex)
                        {
                            LogError(ex);
                        }
                    }
                }
            }
            // reset Parameters, but keep the same query in case the user wants to re-run it
            this.Query = this.Query;
            return results;
        }

        public async Task<List<object[]>> ExecuteReaderAsync()
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(Query))
            {
                return new List<object[]>();
            }
            List<object[]> results = new List<object[]>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    // Add parameters to the command.
                    int paramsAdded = 0;
                    foreach (string key in Parameters.Keys)
                    {
                        try
                        {
                            object value = Parameters[key];
                            command.Parameters.AddWithValue(key, value);
                            paramsAdded++;
                        }
                        // I'm not even sure this is possible, but if KeyNotFoundException was to happen
                        // the query would be almost certainly unsuccessful due to the missing parameter
                        // TODO: determine whether or not to catch ArgumentNullExceptions. 
                        catch (KeyNotFoundException ex)
                        {
                            LogError(ex);
                            return results;
                        }
                        // Might be better to abort the query in case of some unforseen error.
                        catch (Exception ex)
                        {
                            LogError(ex);
                            return results;
                        }
                    }
                    // All parameters should have been added to command. Abort in case of some forseen error.
                    if (paramsAdded != Parameters.Count)
                    {
                        return results;
                    }

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        int i = 0;
                        while (reader.Read())
                        {
                            IDataRecord currentRow = reader;
                            results.Add(new object[currentRow.FieldCount]);
                            for (int j = 0; j < currentRow.FieldCount; j++)
                            {
                                results[i][j] = currentRow[j];
                            }
                            i++;
                        }
                        reader.Close();
                    }
                    catch (ObjectDisposedException ex)
                    {
                        LogError(ex);
                    }
                    catch (InvalidCastException ex)
                    {
                        LogError(ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        LogError(ex);
                    }
                    catch (IOException ex)
                    {
                        LogError(ex);
                    }
                    catch (SqlException ex)
                    {
                        LogError(ex);
                    }
                    finally
                    {
                        try
                        {
                            connection.Close();
                        }
                        catch (SqlException ex)
                        {
                            LogError(ex);
                        }
                    }
                }
            }
            // reset Parameters, but keep the same query in case the user wants to re-run it
            this.Query = this.Query;
            return results;
        }
    }
}
