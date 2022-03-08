using System;
using Xunit;
using UserManagement;

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


            string user = "mkriesel";
            bool isDeleted = UserManager.DeleteUser(user);
            Assert.True(isDeleted);


        }
    }
}
