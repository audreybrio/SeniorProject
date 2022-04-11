using System;
using Xunit;
using StudentMultiTool.Backend.Services.Users;

namespace UserAccTest
{
    public class UnitTest1
    {
        [Fact]
        public void Success()
        {
            UserAccount aud = new UserAccount("Audrey Brio", "abrio", "Pas$word1", "audrey.brio@student.csulb.edu", "1234", "Admin", true);
            aud.email = "michael.kriesel@student.csulb.edu";
            aud.username = "bncikle";
            string expectedEmail = "michael.kriesel@student.csulb.edu";
            string expectedUsername = "bnickle";
            string expectedName = "Audrey Brio";
            Assert.Equal(expectedEmail, "michael.kriesel@student.csulb.edu");
            Assert.Equal(expectedUsername, "bnickle");
            Assert.Equal(expectedName, "Audrey Brio");

        }
    }
}
