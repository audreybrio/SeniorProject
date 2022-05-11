using System;
using Xunit;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StudentMultiTool.Backend.Services.DataAccess;

namespace Tests.Backend.Services.DataAccess
{
    public class SQLCommandRunnerTest
    {
        [Fact]
        public void ConnectionString()
        {
            string x = "x";
            string y = "y";
            string z = "z";
            SqlCommandRunner runnerX = new SqlCommandRunner(x);
            SqlCommandRunner runnerY = new SqlCommandRunner(y);
            SqlCommandRunner runnerZ = new SqlCommandRunner(z);

            Assert.Equal(x, runnerX.ConnectionString);
            Assert.Equal(y, runnerY.ConnectionString);
            Assert.Equal(z, runnerZ.ConnectionString);
            Assert.NotEqual(x, runnerY.ConnectionString);
            Assert.NotEqual(x, runnerZ.ConnectionString);
            Assert.NotEqual(y, runnerZ.ConnectionString);
        }

        [Fact]
        public void Query()
        {
            string x = "x";

            // This is a dummy connection string
            SqlCommandRunner runner = new SqlCommandRunner("Server=localhost;Initial Catalogue=testdb;Authentication=false;");
            SqlCommandRunner runnerX = new SqlCommandRunner(x);
            string query = "SELECT name FROM myTable WHERE name=@name";
            runner.Query = query;
            runnerX.Query = query;
            Assert.Equal(query, runner.Query);
            Assert.Equal(query, runnerX.Query);
        }
        [Fact]
        public void AddParams()
        {
            // This is a dummy connection string
            SqlCommandRunner runner = new SqlCommandRunner("Server=localhost;Initial Catalogue=testdb;Authentication=false;");
            string query = "SELECT name FROM myTable WHERE name=@name";
            string paramName = "@name";
            string paramValue = "John";
            Dictionary<string, object> expected = new Dictionary<string, object>();
            runner.AddParam(paramName, paramValue);
            expected.Add(paramName, paramValue);
            Assert.Equal(expected, runner.Parameters);
        }
    }
}
