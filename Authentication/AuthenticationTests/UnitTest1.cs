using System;
using Xunit;
using Authentication;

namespace AuthenticationTests
{
    public class UnitTest1
    {
        // Login Successful
        [Fact]
        public void LoginSuccessfulTest()
        {
            string email = "audrey.brio@student.csulb.edu";
            string passcode = "1234";
            bool log;
            log = Authenticate.Authen(email, passcode);
            Assert.True(log);

            string username = "abrio";
            string password = "Pas$word1";
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
            string passcode = "1234";
            bool log;
            log = Authenticate.Authen(email, passcode);
            Assert.True(log);


            string username = "abrio";
            string password = "Pas$word1";
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
            string passcode = "12345678";
            bool log;
            log = Authenticate.Authen(email, passcode);
            Assert.True(log);

            string username = "abrio";
            string password = "Pas$word1";
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
            string passcode = "12345";
            bool log;
            log = Authenticate.Authen(email, passcode);
            Assert.True(log);

            string username = "bnickle";
            string password = "Pas$word2";
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
            string passcode = "123456";
            bool log;
            log = Authenticate.Authen(email, passcode);
            Assert.True(log);

            string username = "mk";
            string password = "Pas$word3";
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
            string passcode = "123456";
            bool log;
            log = Authenticate.Authen(email, passcode);
            Assert.True(log);

            string username = "mkriesel";
            string password = "Pas$word1";
            int temp;
            temp = Validate.LoginUser(username, password);
            bool t = Evaluate.EvaluateBool(temp);
            Assert.True(t);
        }
    }
}
