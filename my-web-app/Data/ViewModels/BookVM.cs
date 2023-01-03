namespace my_web_app.Data.ViewModels
{
    // view models eto to chto vidjat useri za predelami backenda, toest im nahuj ne nuzhno viditj veshi kak id ili loging veshi kak dateadded
    public class BookVM
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool isRead { get; set; }

        public DateTime? DateRead { get; set; }

        public int? Rate { get; set; }

        public string CoverUrl { get; set; }

        public string Genre { get; set; }

        //

        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; }

    }

    public class BookWithAuthorsVM
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool isRead { get; set; }

        public DateTime? DateRead { get; set; }

        public int? Rate { get; set; }

        public string CoverUrl { get; set; }

        public string Genre { get; set; }

        //

        public string PublisherName { get; set; }
        public List<string> AuthorNames { get; set; }

    }
}
