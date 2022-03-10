using StudentMultiTool.Backend.Services.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureDemo
{
    public class RegistrationDemo
    {
        public RegistrationDemo()
        {
            string userName;
            string password;
            string email;
            string university;
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
                university = System.Console.ReadLine();
            } while (!input.validateSchool(university));

            //UserAccount userAcc = new UserAccount(userName, password, email, university);
            System.Console.ReadLine();
        }
    }
}
