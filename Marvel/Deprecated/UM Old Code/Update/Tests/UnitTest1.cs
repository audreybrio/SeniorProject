using System;
using Xunit;
using Rextester;

namespace UpdateTests
{
    public class UnitTest1
    {
        // Update Student to Admin 
        [Fact]
        public void UpdateStudentToAdmin()
        {
            string username = "mkriesel";
            string role = "admin";
            bool temp = Program.updatetUserRole(username, role);
            Assert.True(temp);
            
        }
        // Update Admin to Student
        [Fact]
        public void UpdateAdminToStudent()
        {
            string username = "atoscano";
            string role = "student";
            bool temp = Program.updatetUserRole(username, role);
            Assert.True(temp);

        }

        // Update Admin to Admin
        [Fact]
        public void UpdateAdminToAdmin()
        {
            string username = "abrio";
            string role = "admin";
            bool temp = Program.updatetUserRole(username, role);
            Assert.True(temp);

        }

        // Update Student to Student 
        [Fact]
        public void UpdateStudentToStudent()
        {
            string username = "jcutri";
            string role = "student";
            bool temp = Program.updatetUserRole(username, role);
            Assert.True(temp);

        }

        // Update User who doesnt exist 
        [Fact]
        public void UpdateNoUser()
        {
            string username = "mk";
            string role = "student";
            bool temp = Program.updatetUserRole(username, role);
            Assert.True(temp);

        }
    }
}
