using StudentMultiTool.Backend.Services.Authentication.Controller;

using StudentMultiTool.Backend.Services.BookSelling;

namespace ConsoleDemo
{
    public class ConsoleDemo
    {
        public static void Main(string[] args)
        {
            // Console customization
            // Change the look of the console
            Console.Title = "StudentMultiTool";
            // Change console text color
            Console.ForegroundColor = ConsoleColor.Cyan;
            // Change terminal height
            Console.WindowHeight = 40;

            // Create UiPrint Object
            UiPrint ui = new UiPrint();

            // Create bool object for menu loop
            bool menuLoop = true;
            // Create loop for menu
            while (menuLoop is true)
            {
                // Print User Management menu
                ui.SystemAccountMenu();
                // Get user choice
                int menuChoice = Convert.ToInt32(Console.ReadLine());
                // Complete the appropriate action
                switch (menuChoice)
                {
                    // Exit menu
                    case 0:
                        menuLoop = false;
                        break;
                    // Registration
                    case 1:

                        break;
                    // Login/Logout
                    case 2:
                        LoginController loginController = new LoginController();
                        loginController.Authenticate();
                        break;
                    // Schedule Builder
                    case 3:
                        ScheduleBuilderDemo demo = new ScheduleBuilderDemo();
                        demo.Demo();
                        break;
                    // Book Selling
                    case 4:
                        BookSellingController bookList = new BookSellingController();
                        break;
                    // Automated Moderating
                    case 5:
                        break;
                    // User Access Control
                    case 6:
                        break;
                    // Usage Analysis Dashboard
                    case 7:
                        break;
                    default:
                        Console.WriteLine("Invalid input.\nPlease enter a valid option.\n");
                        break;
                }
            }
        }
    }
}