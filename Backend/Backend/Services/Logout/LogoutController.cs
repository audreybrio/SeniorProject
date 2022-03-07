﻿using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Logout()
        {
            Console.WriteLine("Welcome to Student Multi-Tool Home Page");
            Console.Title = "StudentMultiTool Home";
            // Change console text color
            Console.ForegroundColor = ConsoleColor.Cyan;
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
                    case 0:
                        menuLoop = false;
                        break;
                    case 1:
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
