using System;
using Xunit;
using UserManagement;

namespace CreateTesting
{
    public class CreateUnitTesting
    {

        // Creating User with valid username and passcode 
        [Fact]
        public void CreatingUser()
        {
            string name = "Joe Man";
            string user = "jmann";
            string passcode = "spiderman";
            string email = "asb@edu";
            string password = "12345";
            bool isCreated = UserManager.CreateUsers(name, user, password, email, passcode);
            Assert.True(isCreated);

        }

        // Creating user with username that already exists
        [Fact]
        public void UsernameTaken()
        {
            string name = "Palo Nick";
            string user = "bnickle";
            string passcode = "spiderman";
            string email = "asb@edu";
            string password = "12345";
            bool isCreated = UserManager.CreateUsers(name, user, password, email, passcode);
            Assert.True(isCreated);

        }

        // Creating user with invalid username 
        [Fact]
        public void InvaliUsername()
        {
            string name = "Happy Man";
            string user = "jb";
            string passcode = "spider man";
            string email = "asb@edu";
            string password = "12345";
            bool isCreated = UserManager.CreateUsers(name, user, password, email, passcode);
            Assert.True(isCreated);

        }

        // Creating User with invald passcode 
        [Fact]
        public void InvalidPasscode()
        {
            string name = "Peter Parker ";
            string user = "spidy";
            string passcode = "no";
            string email = "asb@edu";
            string password = "12345";
            bool isCreated = UserManager.CreateUsers(name, user, password, email, passcode);
            Assert.True(isCreated);

        }
    }
}
