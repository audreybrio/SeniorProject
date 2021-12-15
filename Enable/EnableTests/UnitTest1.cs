using System;
using Xunit;
using Enable;

namespace EnableTests
{
    public class UnitTest1
    {
        
        // Enabling a User that is currently Disabled 
        [Fact]
        public void EnableDisabledUser()
        {
            string username = "bnickle";
            bool t = EnableUser.userExist(username);
            Assert.True(t);

        }

        // Enabling User that does not exist 
        [Fact]
        public void UserNotExist()
        {
            string username = "bk";
            bool t = EnableUser.userExist(username);
            Assert.True(t);

        }

        //Enabling a user that is already enabled
        [Fact]
        public void UserAlreadyEnabled()
        {
            string username = "jcutri";
            bool t = EnableUser.userExist(username);
            Assert.True(t);

        }
    }
}
