using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using StudentMultiTool.Backend.Services.UserManagement;
using StudentMultiTool.Backend.Services.DataAccess;
using UserAcc;

namespace Tests.Backend.Services.UserManagement
{
    public class BulkOperationInterpreterTest
    {
        [Fact]
        public void Example()
        {
            BulkOperationInterpreter boi = new BulkOperationInterpreter();
            string filePath = "./examplecommands.txt";
            string result = boi.Execute(filePath);
            Assert.Equal(boi.Success, result);
        }
    }
}
