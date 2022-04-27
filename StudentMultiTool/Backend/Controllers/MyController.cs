using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using StudentMultiTool.Backend.Services.DataAccess;

namespace StudentMultiTool.Backend.Controllers
{
    // Contains debug actions
    [ApiController]
    [Route("api/mycontroller")]
    public class MyController : ControllerBase
    {
        [HttpGet]
        public IActionResult UtcNow()
        {
            return Ok(DateTime.UtcNow);
        }

        [HttpGet]
        public IActionResult ControllersWorking()
        {
            return Ok("Controllers are working");
        }

        [HttpGet("[action]/{text}")]
        public IActionResult Echo(string text)
        {
            return Ok(text);
        }
        [HttpGet("schools")]
        public async Task<IActionResult> SchoolsAsync()
        {
            string result = string.Empty;
            string? connectionString = Environment.GetEnvironmentVariable(EnvironmentVariableEnum.CONNECTIONSTRING);
            if (connectionString == null)
            {
                return Ok("Oops");
            }
            try
            {
                List<string> results = new List<string>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * from Schools;";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            IDataRecord row = reader;
                            //results.Add(reader.GetString(0));
                            string? current = (string) row[0];
                            if (current != null)
                            {
                                results.Add(current);
                            }
                        }
                        connection.Close();
                    }
                }
                foreach (string s in results)
                {
                    result = result + s + "\n";
                }
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine("The following exception has occurred: " +
                        ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("The following exception has occurred: " +
                        ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
            return Ok(result);
        }
    }
}
