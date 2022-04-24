using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Services.BookSelling
{
    public class BookSellingController
    {
        // Set up database variables
        public string sql { get; set; }
        public string? dbConnectionString { get; set; } = "";
        public BookSellingController()
        {
            
            dbConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");

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
                        var formCreate = BookListForm(userN);
                        bool createList = CreateListing(formCreate);
                        break;
                    // Update a Listing
                    case 2:
                        // Get form text fields
                        var formUpdate = BookListForm(userN);
                        bool updateList = UpdateListing(formUpdate);
                        break;
                    // Remove a Listing
                    case 3:
                        // Get form text fields
                        var formDelete = BookListForm(userN);
                        bool deleteList = DeleteListing(formDelete);
                        break;
                    // Search for a Listing by title
                    case 4:
                        // Search for book based on title given
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
                using (SqlConnection connection = new SqlConnection(dbConnectionString))
                {
                    string query = $"INSERT INTO BookListings (UserName,BookTitle, BookDescription, BookEdition, BookIsbn, Contact, ListDate, IsActive)" +
                    $"VALUES ('{book.username}', " +
                    $"'{book.bookTitle}','{book.bookDes}','{book.bookEdition}'," +
                    $"'{book.isbn}', '{book.contactInfo}', '{DateTime.UtcNow}', 1);";

                    int rowsAffected = 0;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        try
                        {
                            connection.Open();
                            rowsAffected = command.ExecuteNonQuery();
                            connection.Close();
                            return true;
                        }
                        catch (System.InvalidOperationException ex)
                        {
                            return false;
                            Console.WriteLine("The following exception has occurred: " +
                                    ex.GetType().FullName);
                            Console.WriteLine(ex.Message);
                        }
                        catch (SqlException ex)
                        {
                            return false;
                            Console.WriteLine("The following exception has occurred: " +
                                    ex.GetType().FullName);
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Update a Listing
        public bool UpdateListing(BookListings book)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dbConnectionString))
                {
                    string query = $"UPDATE BookListings SET UserName = '{book.username}',BookTitle = '{book.bookTitle}', " +
                    $"BookDescription = '{book.bookDes}', BookEditio = '{book.bookEdition}', " +
                    $"BookIsbn = '{book.isbn}', Contact = '{book.contactInfo}'" +
                    $"Where UserName = '{book.username}' AND BookTitle = '{book.bookTitle}'";

                    int rowsAffected = 0;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        try
                        {
                            connection.Open();
                            rowsAffected = command.ExecuteNonQuery();
                            connection.Close();
                            return true;
                        }
                        catch (System.InvalidOperationException ex)
                        {
                            return false;
                            Console.WriteLine("The following exception has occurred: " +
                                    ex.GetType().FullName);
                            Console.WriteLine(ex.Message);
                        }
                        catch (SqlException ex)
                        {
                            return false;
                            Console.WriteLine("The following exception has occurred: " +
                                    ex.GetType().FullName);
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Delete a Listing
        public bool DeleteListing(BookListings book)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dbConnectionString))
                {
                    string query = $"DELETE FROM BookListings " +
                    $"Where UserName = '{book.username}' AND BookTitle = '{book.bookTitle}'";

                    int rowsAffected = 0;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        try
                        {
                            connection.Open();
                            rowsAffected = command.ExecuteNonQuery();
                            connection.Close();
                            return true;
                        }
                        catch (System.InvalidOperationException ex)
                        {
                            return false;
                            Console.WriteLine("The following exception has occurred: " +
                                    ex.GetType().FullName);
                            Console.WriteLine(ex.Message);
                        }
                        catch (SqlException ex)
                        {
                            return false;
                            Console.WriteLine("The following exception has occurred: " +
                                    ex.GetType().FullName);
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }
        // Search for a listing
        public bool SearchTitleListing()
        {
            Console.WriteLine("Please enter the title of the book to search for:");
            string searchTitle = Console.ReadLine();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("MARVELCONNECTIONSTRING");
            conn.Open();
            SqlCommand cmd = new SqlCommand($"SELECT * from BookListings WHERE BookTitle = '{searchTitle}';", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int rowCounts = 0;
            reader.Close();
            rowCounts = (int)cmd.ExecuteScalar();

            if (rowCounts > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Get Form information
        public BookListings BookListForm(string userName)
        {
/*            BookListings newListing = new BookListings();*/
            // Get Form text fields
            BookSellingUi ui = new BookSellingUi();
            var newListing = ui.BookFormMenu(userName);

            return newListing;
        }
    }
    // Create Listing objects
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
