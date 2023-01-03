
namespace my_web_app.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        //Navigation Property
        public List<Book_Author> Book_Authors{ get; set; }
    }
}
