using System;
using Xunit;
using StudentMultiTool.Backend.Services.UserManagement;

namespace DeleteTesting
{
    public class DeleteUnitTests
    {
       
        // Deleting User that does not exist 
        [Fact]
        public void DeleteUserFail()
        {


            string user = "mkries";
            bool isDeleted = UserManager.DeleteUser(user);
            Assert.False(isDeleted);


        }

        // Deleting a user successfully
        [Fact]
        public void DeleteUserSuccess()
        {


            string user = "jcutri";
            bool isDeleted = UserManager.DeleteUser(user);
            Assert.True(isDeleted);


        }
    }
}
