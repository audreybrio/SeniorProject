using System;
using Xunit;
using Disable;

namespace DisableTests
{
    public class UnitTest1
    {
        [Fact]
        public void DisablEnabledUser()
        {
            string username = "jcutri";
            bool disableUser = DisableUser.userExist(username);
            Assert.True(disableUser);
        }

        [Fact]
        public void DisableDisabledUser()
        {
            string username = "bnickle";
            bool disableUser = DisableUser.userExist(username);
            Assert.True(disableUser);
        }

        [Fact]
        public void DisableNoUser()
        {
            string username = "kms";
            bool disableUser = DisableUser.userExist(username);
            Assert.True(disableUser);
        }
    }
}
