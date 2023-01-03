using my_web_app.Data.Models;
using my_web_app.Data.ViewModels;

namespace my_web_app.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM authorVM)
        {
            var _author = new Author()
            {
                FullName = authorVM.FullName,
            };

            _context.Authors.Add(_author);
            _context.SaveChanges();

        }

        public AuthorWithBooksVM GetAuthorWithBooks(int id)
        {
            var _author = _context.Authors.Where(n => n.Id == id).Select(n => new AuthorWithBooksVM
            {
                FullName = n.FullName,
                BookTitles = n.Book_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return _author;
        }
    }
}
