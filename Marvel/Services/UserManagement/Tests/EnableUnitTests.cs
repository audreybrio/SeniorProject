using System;
using Xunit;
using UserManagement;

namespace EnableTests
{
    public class EnableUnitTests
    {
        // Enabling a User that is currently Disabled 
        [Fact]
        public void EnableDisabledUser()
        {
            string username = "bnickle";
            bool isEnabled = UserManager.EnableUser(username);
            Assert.True(isEnabled);

        }

        // Enabling User that does not exist 
        [Fact]
        public void UserNotExist()
        {
            string username = "bk";
            bool isEnabled = UserManager.EnableUser(username);
            Assert.True(isEnabled);

        }

        //Enabling a user that is already enabled
        [Fact]
        public void UserAlreadyEnabled()
        {
            string username = "jcutri";
            bool isEnabled = UserManager.EnableUser(username);
            Assert.True(isEnabled);

        }
    }
}
