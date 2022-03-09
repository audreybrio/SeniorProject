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
            Console.WriteLine((menu) + ". Create a listing.");
            menu += 1;
            Console.WriteLine((menu) + ". Update a listing.");
            menu += 1;
            Console.WriteLine((menu) + ". Remove a listing.");
            menu += 1;
            Console.WriteLine((menu) + ". Search for a Listing by title.");
            
        }
        public BookListings BookFormMenu(string uN)
        {
           
            // Menu for all book form fields
            Console.WriteLine("Please enter the name of the book:");
            string bTitle = Console.ReadLine();
            Console.WriteLine("Please enter the condition of the book(New,Used):");
            string bDescription = Console.ReadLine();
            Console.WriteLine("Please enter the edition of the book(enter '1' if unknown):");
            int bEdition = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter the ISBN-10 number of the book:");
            string bIsbn = Console.ReadLine();
            Console.WriteLine("Please enter your preferred contact information(email or phone number):");
            string bContact = Console.ReadLine();

            // Create object for listing fields
            BookListings listing = new BookListings(uN, bTitle, bEdition, bIsbn, bDescription, bContact);

            return listing;
        }
    }
}
