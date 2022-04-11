using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace StudentMultiTool.Backend.Services.UsageAnalysisDashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsageanalysisController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsageanalysisController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpGet]
        public JsonResult Getdata()
        {
            string query = @"
                           select View_name,No_view from dbo.t2
                            ";
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    myReader = cmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}
