using System;
using Xunit;
using UserManagement;

namespace UpdateTesting
{
    public class UpdateUnitTests
    {
        // Update Student to Admin 
        [Fact]
        public void UpdateStudentToAdmin()
        {
            string username = "mkriesel";
            string role = "admin";
            bool isUpdated = UserManager.UpdateRoleUser(username, role);
            Assert.True(isUpdated);

        }
        // Update Admin to Student
        [Fact]
        public void UpdateAdminToStudent()
        {
            string username = "atoscano";
            string role = "student";
            bool isUpdated = UserManager.UpdateRoleUser(username, role);
            Assert.True(isUpdated);

        }

        // Update Admin to Admin
        [Fact]
        public void UpdateAdminToAdmin()
        {
            string username = "abrio";
            string role = "admin";
            bool isUpdated = UserManager.UpdateRoleUser(username, role);
            Assert.True(isUpdated);

        }

        // Update Student to Student 
        [Fact]
        public void UpdateStudentToStudent()
        {
            string username = "jcutri";
            string role = "student";
            bool isUpdated = UserManager.UpdateRoleUser(username, role);
            Assert.True(isUpdated);

        }

        // Update User who doesnt exist 
        [Fact]
        public void UpdateNoUser()
        {
            string username = "mk";
            string role = "student";
            bool isUpdated = UserManager.UpdateRoleUser(username, role);
            Assert.True(isUpdated);

        }
    }
}
