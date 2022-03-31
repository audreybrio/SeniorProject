using StudentMultiTool.Backend.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    public class UiPrint
    {
        public void SystemAccountMenu()
        {
            // Console customization
            // Change the look of the console
            Console.Title = "StudentMultiTool";
            // Change console text color
            Console.ForegroundColor = ConsoleColor.Cyan;
            // Change terminal height
            Console.WindowHeight = 40;
            // Menu for all UserManagement options
            int menu = 0;
            Console.WriteLine("This menu is for the purpose of demonstrating the features.\n");
            Console.WriteLine("Please select an option.");
            Console.WriteLine("0. To exit.");
            Console.WriteLine("1. Registration.");
            Console.WriteLine("2. Login/Logout.");
            Console.WriteLine("3. Schedule Builder.");
            Console.WriteLine("4. Book Selling.");
            Console.WriteLine("5. Automated Moderating.");
            Console.WriteLine("6. User Access Control.");
            Console.WriteLine("7. Usage Anaylsis Dashboard.");
        }
    }
}
