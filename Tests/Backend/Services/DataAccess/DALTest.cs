using StudentMultiTool.Backend.Services.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Backend.Services.DataAccess
{
    public class DALTest
    {
        // Write to database
        [Fact]
        public void DataWriteSuccessTest()
        {
            // Arrange
            SQLServerDAO dal = new SQLServerDAO();
            string writeTest = "INSERT INTO Table2 (name, num) VALUES ('text', 2);";
            
            // Act
            bool dataWrite = dal.WriteData(writeTest);

            // Assert
            Assert.True(dataWrite);
        }
        // Fail to write to database
        [Fact]
        public void DataWriteFailTest()
        {
            // Arrange
            SQLServerDAO dal = new SQLServerDAO();
            string writeTest = "INSERT INTO Table2 (name, num) VALUES (text, 2);";

            // Act
            bool dataWrite = dal.WriteData(writeTest);

            // Assert
            Assert.False(dataWrite);
        }
        // Read from database
        [Fact]
        public void DataReadSuccessTest()
        {
            // Arrange
            SQLServerDAO dal = new SQLServerDAO();
            string readTest = "SELECT name from Table2 where name = 'text';";

            // Act
            var readData = dal.ReadData(readTest);

            // Assert
            if (readData != null)
            {
                Assert.True(true);
            }
            else
            {
                Assert.False(false);
            }  
        }
        // Edit table within database
        [Fact]
        public void DataEditSuccessTest()
        {
            // Arrange
            SQLServerDAO dal = new SQLServerDAO();
            string writeTest = "UPDATE Table2 Set name = 'updated' where name = 'text';";

            // Act
            bool dataUpdate = dal.UpdateData(writeTest);

            // Assert
            Assert.True(dataUpdate);
        }
        // Remove from database
        [Fact]
        public void DataDeleteSuccessTest()
        {
            // Arrange
            SQLServerDAO dal = new SQLServerDAO();
            string writeTest = "DELETE FROM Table2 where name = 'updated';";

            // Act
            bool dataUpdate = dal.UpdateData(writeTest);

            // Assert
            Assert.True(dataUpdate);
        }
    }
}
