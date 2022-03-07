namespace StudentMultiTool.Backend.Services.BookSelling
{
    public class BookSellingUi
    {
        public void BookListingMenu()
        {
            // Menu for all UserManagement options
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
