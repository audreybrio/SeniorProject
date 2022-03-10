using StudentMultiTool.Backend.Services.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAcc;
using UserManagement;

namespace FeatureDemo
{
    public class RegistrationDemo
    {
        public RegistrationDemo()
        {
            string userName;
            string password;
            string email;
            string school;
            InputValidation input = new InputValidation();

            System.Console.WriteLine("\tRegistration\n");
            do
            {
                System.Console.Write("Email address: ");
                email = System.Console.ReadLine();
            } while (!input.validateEmail(email));

            do
            {
                System.Console.Write("Password: ");
                password = System.Console.ReadLine();
            } while (!input.validatePassword(password));

            do
            {
                System.Console.Write("Username: ");
                userName = System.Console.ReadLine();
            } while (!input.validateUsername(userName));


            do
            {
                System.Console.Write("University: ");
                school = System.Console.ReadLine();
            } while (!input.validateSchool(school));

            UserAccount userAcc = new UserAccount(email, password, userName, school);
            Update usertoDB = new Update();
            usertoDB.UpdateCreate(email, password, userName, school);
        }
    }
}
