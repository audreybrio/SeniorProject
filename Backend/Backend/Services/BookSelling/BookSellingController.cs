using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Services.BookSelling
{
    public class BookSellingController
    {
        // Set up database variables
        public string sql { get; set; }
        public SqlConnection _connection { get; set; }
        public BookSellingController()
        {
            
            sql = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            _connection = new SqlConnection(sql);

            // Console customization
            // Change the look of the console
            Console.Title = "StudentMultiTool";
            // Change console text color
            Console.ForegroundColor = ConsoleColor.Green;
            // Change terminal height
            Console.WindowHeight = 40;

            // Create ui object
            BookSellingUi ui = new BookSellingUi();

            // Show Initial menu and get user information
            string userN = ui.BookListStart();

            // Create bool object for menu loop
            bool bookLoop = true;
            // Create loop for menu
            while (bookLoop is true)
            {
                //Print the menu for book selling
                ui.BookListingMenu();
                // Get user choice
                int menuChoice = Convert.ToInt32(Console.ReadLine());
                // Complete the appropriate action
                switch (menuChoice)
                {
                    // Exit menu
                    case 0:
                        bookLoop = false;
                        break;
                    // Create a Listing
                    case 1:
                        // Get form text fields
                        var form = BookListForm();
                        bool createList = CreateListing();
                        break;
                    // Update a Listing
                    case 2:
                        bool updateList = UpdateListing();
                        break;
                    // Remove a Listing
                    case 3:
                        bool deleteList = DeleteListing();
                        break;
                    // Search for a Listing by title
                    case 4:
                        bool searchTitleList = SearchTitleListing();
                        break;
                    default:
                        Console.WriteLine("Invalid input.\nPlease enter a valid option.\n");
                        break;
                }
            }

        }
        // Create a Lisitng 
        public bool CreateListing(BookListings book)
        {
            try
            {
                // Insert into users table
                string sqlUser = $"INSERT INTO bookListings (UserName,BookTitle, BookDescription, BookEdition, BookIsbn, Contact, ListDate, IsActive)" +
                    $"VALUES ('{book.username}', " +
                    $"'{book.bookTitle}','{book.bookDes}','{book.bookEdition}'," +
                    $"'{book.isbn}', '{book.contactInfo}', NOW(), 1);";

                bool insertNewUser = dbSource.WriteData(sqlUser);
                return insertNewUser;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // update a Listing
        public bool UpdateListing(BookListings book)
        {
            try
            {
                // Insert into users table
                string sqlUser = $"INSERT INTO bookListings (UserName,BookTitle, BookDescription, BookEdition, BookIsbn, Contact, ListDate, IsActive)" +
                    $"VALUES ('{book.username}', " +
                    $"'{book.bookTitle}','{book.bookDes}','{book.bookEdition}'," +
                    $"'{book.isbn}', '{book.contactInfo}', NOW(), 1);";

                bool insertNewUser = dbSource.WriteData(sqlUser);
                return insertNewUser;
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        // Delete a Listing
        public bool DeleteListing(BookListings book)
        {
            return false;
        }
        public bool SearchTitleListing()
        {
            return false;
        }
        // 
        public BookListings BookListForm()
        {
            BookListings newListing = new BookListings();
            Console.WriteLine("Please provide your username.\n");
            string username = Console.ReadLine();

            return newListing;
        }
    }
    public class BookListings
    {
        private string _userName;   // Username of seller
        private string _bookTitle;  // Book name
        private string _bookDescription;    // Book condition
        private int _bookEdition; // Book edition
        private string _bookIsbn;
        private DateTime _listDate;
        private string _sellerInfo;


        public BookListings(string un, string bTitle, int bEd, string isbnTen, string bStat, string cInfo)
        {
            _userName = un;
            _bookTitle = bTitle;
            _bookDescription = bStat;
            _bookEdition = bEd;
            _bookIsbn = isbnTen;
            _listDate = DateTime.UtcNow;
            _sellerInfo = cInfo;
        }
        // If nothing is passed, then set all string values to "information missing"
        // Ask user to redo form
        public BookListings()
        {
            _userName = "Information missing";
            _bookTitle = "Information missing";
            _bookDescription = "Information missing";
            _bookEdition = 1;
            _bookIsbn = "Information missing";
            _listDate = DateTime.UtcNow;
            _sellerInfo = "Information missing";
        }
        // Initialize public variables for class objects
        public string username { get { return _userName; } }
        public string bookTitle { get { return _bookTitle; } }
        public string bookDes { get { return _bookDescription; } }
        public string isbn { get { return _bookIsbn; } }
        public int bookEdition { get { return _bookEdition; } }
        public DateTime listDate { get { return _listDate; } }
        public string contactInfo { get { return _sellerInfo; } }

    }
}
