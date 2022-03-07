namespace StudentMultiTool.Backend.Services.BookSelling
{
    public class BookSellingController
    {
        public BookSellingController()
        {
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
                    default:
                        Console.WriteLine("Invalid input.\nPlease enter a valid option.\n");
                        break;
                }
            }

        }
        public bool CreateListing(BookListings book)
        {
            try
            {
                // Insert into users table
                string sqlUser = $"INSERT INTO users (UserName, Password, Role, IsActive," +
                    $"CreatedBy, CreatedDate, Email) VALUES ('{newUser.username}', " +
                    $"'{newUser.password}','{newUser.role}', 1," +
                    $"'{CreatedBy}', NOW(),{newUser.email});";

                bool insertNewUser = dbSource.WriteData(sqlUser);
                return insertNewUser;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateListing()
        {
            return false;
        }
        public bool DeleteListing()
        {
            return false;
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
        private int _activePost;


        public BookListings(string un, string bTitle, int bEd, string isbnTen, string bStat, string cInfo)
        {
            _userName = un;
            _bookTitle = bTitle;
            _bookDescription = bStat;
            _bookEdition = bEd;
            _bookIsbn = isbnTen;
            _listDate = DateTime.UtcNow;
            _sellerInfo = cInfo;
            _activePost = 1;
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
            _activePost = 0;
        }
        public string username { get { return _userName; } }
        public string bookTitle { get { return _bookTitle; } }
        public string bookDes { get { return _bookDescription; } }
        public string isbn { get { return _bookIsbn; } }
        public int bookEdition { get { return _bookEdition; } }
        public DateTime listDate { get { return _listDate; } }
        public string contactInfo { get { return _sellerInfo; } }
        public int listStatus { get { return _activePost; } }

    }
}
