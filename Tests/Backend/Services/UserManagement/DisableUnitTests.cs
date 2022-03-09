using System;
using Xunit;
using UserManagement;

namespace DisableTesting
{
    public class DisableUnitTests
    {

        // Disable a User that is currently enabled
        [Fact]
        public void DisablEnabledUser()
        {
            string username = "jcutri";
            bool isDisabled = UserManager.DisableUser(username);
            Assert.True(isDisabled);
        }

        // Disable a User that is already disabled
        [Fact]
        public void DisableDisabledUser()
        {
            string username = "jmann";
            bool isDisabled = UserManager.DisableUser(username);
            Assert.False(isDisabled);
        }

        // Disable User that doesnt Exist
        [Fact]
        public void DisableNoUser()
        {
            string username = "kms";
            bool isDisabled = UserManager.DisableUser(username);
            Assert.False(isDisabled);
        }
    }
}
