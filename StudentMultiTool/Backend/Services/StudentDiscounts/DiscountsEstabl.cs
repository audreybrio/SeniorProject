namespace StudentMultiTool.Backend.Services.StudentDiscounts
{
    // Establishment Discounts class
    public class DiscountsEstabl
    {
        private int _id;
        private string? _title, _address, _description;
        private string? _date, _name, _lat, _lng;
        private int _likes, _dislikes;

        public DiscountsEstabl(int Id, string Name, string Title, string Address, string Lat, string Lng,
                                string Description, string Date, int Likes, int Dislikes)
        {
            this.Id = Id;
            this.Name = Name;
            this.Title = Title;
            this.Address = Address;
            this.Lat = Lat;
            this.Lng = Lng;
            this.Description = Description;
            this.Date = Date;
            this.Likes = Likes;
            this.Dislikes = Dislikes;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String? Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public String? Title
        {
            get { return _title; }
            set { _title = value; }
        }



        public string? Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string? Lat
        {
            get { return _lat; }
            set { _lat = value; }
        }
        public string? Lng
        {
            get { return _lng; }
            set { _lng = value; }
        }
        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string? Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public int Likes
        {
            get { return _likes; }
            set { _likes = value; }
        }

        public int Dislikes
        {
            get { return _dislikes; }
            set { _dislikes = value; }
        }

    }


}
