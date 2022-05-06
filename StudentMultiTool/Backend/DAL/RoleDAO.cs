using System.Data.SqlClient;
using StudentMultiTool.Backend.Services.DataAccess;
using UserAcc;

namespace StudentMultiTool.Backend.DAL
{
    public class RoleDAO
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string Success { get; } = "Success";
        public RoleDAO()
        {
            ConnectionString = Environment.GetEnvironmentVariable(EnvironmentVariableEnum.CONNECTIONSTRING);
        }
        public List<string> SelectAllRoles()
        {
            SqlCommandRunner runner = new SqlCommandRunner(ConnectionString);
            runner.Query = "SELECT RoleName FROM ROLES;";
            List<object[]> data = runner.ExecuteReader();
            List<string> results = new List<string>();
            if (data.Count > 0)
            {
                foreach (object[] row in data)
                {
                    if (row != null && row.Length >= 1)
                    {
                        string temp = (string)row[0];
                        if (!string.IsNullOrEmpty(temp))
                        {
                            results.Add(temp);
                        }
                    }
                }
            }
            else if (data.Count == 0)
            {
                results.Add("admin");
                results.Add("student");
            }
            return results;
        }
    }
}
