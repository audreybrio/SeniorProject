using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Services.Authentication.Controller;

namespace StudentMultiTool.Backend.Services.Logout
{
    public class LogoutController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return new OkResult();
        }
        public  IActionResult Logout()
        {
            Console.WriteLine("Welcome to Student Multi-Tool Home Page");
            Console.Title = "StudentMultiTool Home";
            // Change console text color
            Console.ForegroundColor = ConsoleColor.Green;
            // Change terminal height
            Console.WindowHeight = 40;

            // Create UiPrint Object
            UIPrint ui = new UIPrint();

            // Create bool object for menu loop
            bool menuLoop = true;
            // Create loop for menu
            while (menuLoop is true)
            {
                ui.SystemAccountMenu();
                // Get user choice
                int menuChoice = Convert.ToInt32(Console.ReadLine());
                // Complete the appropriate action
                switch (menuChoice)
                {
                    // Break right away
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        menuLoop = false;
                        break;
                    // Logout - go back to login 
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        LoginController login = new LoginController();
                        login.Authorize();
                        menuLoop = false;
                        break;

                }


            }
            return new OkResult();
        }
    }
}
