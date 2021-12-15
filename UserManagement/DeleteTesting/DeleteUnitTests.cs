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
            bool t = UserManager.DeleteUser(user);
            Assert.True(t);


        }

        // Deleting a user successfully
        [Fact]
        public void DeleteUserSuccess()
        {


            string user = "mkriesel";
            bool t = UserManager.DeleteUser(user);
            Assert.True(t);


        }
    }
}
