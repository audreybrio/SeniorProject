namespace StudentMultiTool.Backend.Services.StudentDiscounts
{

    public class DiscountsWeb
    {
        private int _id;
        private string? _title, _website, _description;
        private string? _date;
        private int _likes, _dislikes;

        public DiscountsWeb(int Id, string Title, string Website, string Description, string Date, int Likes, int Dislikes)
        {
            this.Id = Id;
            this.Title = Title;
            this.Website = Website;
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
        public String? Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string? Website
        {
            get { return _website; }
            set { _website = value; }
        }

        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Date
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
