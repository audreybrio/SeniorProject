namespace StudentMultiTool.Backend.Services.BookSelling
{
    public class BookSellingController
    {
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
        public BookListings()
        {
            _bookTitle = "Information missing";
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
