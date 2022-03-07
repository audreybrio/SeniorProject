using System;
using Xunit;
using Authentication;

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
            log = Authenticate.Authen(email, passcode);
            Assert.True(log);

            string username = "abrio";
            string password = "123456";
            int temp;
            temp = Validate.LoginUser(username, password);
            bool t = Evaluate.EvaluateBool(temp);
            Assert.True(t);
        }

        // Login failed due to incorrect email 
        [Fact]
        public void IncorrectEmailTest()
        {

            string email = "abrio@student.csulb.edu";
            string passcode = "hello world";
            bool log;
            log = Authenticate.Authen(email, passcode);
            Assert.True(log);


            string username = "abrio";
            string password = "123456";
            int temp;
            temp = Validate.LoginUser(username, password);
            bool t = Evaluate.EvaluateBool(temp);
            Assert.True(t);
        }

        // Login failed due to incorrect passcode
        [Fact]
        public void IncorrectPasscodeTest()
        {
            string email = "audrey.brio@student.csulb.edu";
            string passcode = "no";
            bool log;
            log = Authenticate.Authen(email, passcode);
            Assert.True(log);

            string username = "abrio";
            string password = "123456";
            int temp;
            temp = Validate.LoginUser(username, password);
            bool t = Evaluate.EvaluateBool(temp);
            Assert.True(t);
        }

        // Login failed due to account being disabled
        [Fact]
        public void DisabledUserLoginTest()
        {


            string email = "bradley.nickle@student.csulb.edu";
            string passcode = "marvel fan";
            bool log;
            log = Authenticate.Authen(email, passcode);
            Assert.True(log);

            string username = "bnickle";
            string password = "987654";
            int temp;
            temp = Validate.LoginUser(username, password);
            bool t = Evaluate.EvaluateBool(temp);
            Assert.True(t);
        }

        // Login Failure due to incorrect username
        [Fact]
        public void IncorrectUsernameTest()
        {

            string email = "michael.kriesel@student.csulb.edu";
            string passcode = "super man";
            bool log;
            log = Authenticate.Authen(email, passcode);
            Assert.True(log);

            string username = "mk";
            string password = "Password!";
            int temp;
            temp = Validate.LoginUser(username, password);
            bool t = Evaluate.EvaluateBool(temp);
            Assert.True(t);
        }

        // Login failure due to incorrect password
        [Fact]
        public void IncorrectPasswordTest()
        {
            string email = "michael.kriesel@student.csulb.edu";
            string passcode = "super man";
            bool log;
            log = Authenticate.Authen(email, passcode);
            Assert.True(log);

            string username = "mkriesel";
            string password = "Password1";
            int temp;
            temp = Validate.LoginUser(username, password);
            bool t = Evaluate.EvaluateBool(temp);
            Assert.True(t);
        }
    }
}
