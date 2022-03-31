using System;
using Xunit;
using StudentMultiTool.Backend.Services.Authentication.Model;

namespace AuthenticationTests
{
    public class UnitTest1
    {
        // TODO: These are integration tests that will not pass
        // without some setup (inserting values into the database).
        // They should be updated in some way to accommodate this,
        // and/or a test database should be configured with these
        // specific test values.

        // Login Successful
        [Fact]
        public void LoginSuccessfulTest()
        {
            string email = "audrey.brio@student.csulb.edu";
            string passcode = "hello world";
            bool log;
            log = Validate.UserExist(email, passcode);
            Assert.True(log);

            string username = "abrio";
            string password = "123456";
            int temp;
            temp = Validate.LoginUser(username, password);
            bool t = Evaluate.Eval(temp);
            Assert.False(t);
        }

        // Login failed due to incorrect email 
        [Fact]
        public void IncorrectEmailTest()
        {

            string email = "abrio@student.csulb.edu";
            string passcode = "hello world";
            bool log;
            log = Validate.UserExist(email, passcode);
            Assert.False(log);
        }

        // Login failed due to incorrect passcode
        [Fact]
        public void IncorrectPasscodeTest()
        {
            string email = "audrey.brio@student.csulb.edu";
            string passcode = "no";
            bool log;
            log = Validate.UserExist(email, passcode);
            Assert.False(log);


        }

        // Login failed due to account being disabled
        [Fact]
        public void DisabledUserLoginTest()
        {


            string email = "bradley.nickle@student.csulb.edu";
            string passcode = "marvel fan";
            bool log;
            log = Validate.UserExist(email, passcode);
            Assert.False(log);

        }

        // Login Failure due to incorrect username
        [Fact]
        public void IncorrectUsernameTest()
        {

            string email = "jacob.delgado01@student.csulb.edu";
            string passcode = "super man";
            bool log;
            log = Validate.UserExist(email, passcode);
            Assert.True(log);

            string username = "jd";
            string password = "Password!";
            int temp;
            temp = Validate.LoginUser(username, password);
            bool t = Evaluate.Eval(temp);
            Assert.False(t);
        }

        // Login failure due to incorrect password
        [Fact]
        public void IncorrectPasswordTest()
        {
            string email = "jacob.delgado01@student.csulb.edu";
            string passcode = "super man";
            bool log;
            log = Validate.UserExist(email, passcode);
            Assert.True(log);

            string username = "jdelgado";
            string password = "Password1";
            int temp;
            temp = Validate.LoginUser(username, password);
            bool t = Evaluate.Eval(temp);
            Assert.False(t);
        }
    }
}
