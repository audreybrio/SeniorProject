namespace StudentMultiTool.Backend.Services.BookSelling
{
    public class BookSellingUi
    {
        public string BookListStart()
        {
            // Menu for all BookSelling Startup
            Console.WriteLine("Please provide your username.\n");
            // Loop until a username is given
            while (true)
            {
                string username = Console.ReadLine();
                if (username != null)
                {
                    return username;
                }
                else
                {
                    Console.WriteLine("Please enter a username.");
                }
            }
        }
        public void BookListingMenu()
        {
            // Menu for all BookSelling options
            int menu = 0;
            Console.WriteLine("Welcome to Book Selling.\n");
            Console.WriteLine("Please select an option:");
            Console.WriteLine((menu) + ". To exit.");
            menu += 1;
            Console.WriteLine((menu) + ".Create a listing.");
            menu += 1;
            Console.WriteLine((menu) + ". Update a listing.");
            menu += 1;
            Console.WriteLine((menu) + ". Remove a listing.");
        }
        public void BookFormMenu()
        {
            // Menu for all book form fields
            int menu = 0;
            Console.WriteLine("Welcome to Book Selling.\n");
            Console.WriteLine("Please select an option:");
            Console.WriteLine((menu) + ". To exit.");
            menu += 1;
            Console.WriteLine((menu) + ". Registration.");
            menu += 1;
            Console.WriteLine((menu) + ". Login/Logout.");
            menu += 1;
            Console.WriteLine((menu) + ". Schedule Builder.");
            menu += 1;
            Console.WriteLine((menu) + ". Book Selling.");
            menu += 1;
            Console.WriteLine((menu) + ". Automated Moderating.");
            menu += 1;
            Console.WriteLine((menu) + ". User Access Control.");
        }
    }
}
