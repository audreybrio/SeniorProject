using StudentMultiTool.Backend.Services.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAcc;
using UserManagement;
using Xunit;

namespace Tests.Backend.Services.Registration
{
    public class RegistrationTests
    {
        [Fact]
        public void RegistrationSuccess()
        {
            //InputValidation input = new InputValidation();
            //string email = "Audrey.Brio@student.csulb.edu";
            //string userName = "AudreyBrio";
            //string password = "audrey1234";
            //string school = "csulb";
            //UserAccount userAcc = new UserAccount(email, password, userName, school);
            //Update usertoDB = new Update();
            //usertoDB.UpdateCreate(email, password, userName, school);
            //bool successCase = input.emailExists(email);
            //Assert.True(successCase);
        }

        [Fact]
        public void RegistrationInvalidPassword()
        {
            InputValidation input = new InputValidation();
            string email = "Audrey.Brio@student.csulb.edu";
            string userName = "AudreyBrio";
            string password = "audrey";
            string school = "csulb";
            bool validCase = input.validatePassword(password);
            
            Assert.False(validCase);
        }

        [Fact]
        public void RegistrationInvalidEmail()
        {
            InputValidation input = new InputValidation();
            string email = "Audrey.Brio@student.csulb.com";
            string userName = "AudreyBrio";
            string password = "audrey1234";
            string school = "csulb";
            bool validCase = input.validateEmail(email);

            Assert.False(validCase);
        }
    }
}
