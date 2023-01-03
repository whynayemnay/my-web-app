using my_web_app.Data.Models;
using my_web_app.Data.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading;

namespace my_web_app.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBook(BookVM bookVM)
        {
            var _book = new Book()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                isRead = bookVM.isRead,
                DateRead = bookVM.isRead ? bookVM.DateRead.Value : null,
                Rate = bookVM.isRead ? bookVM.Rate.Value : null,
                Genre = bookVM.Genre,
                CoverUrl = bookVM.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = bookVM.PublisherId,
            };
            
            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach (var id in bookVM.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _context.Books_Authors.Add(_book_author);
                _context.SaveChanges();
            }

        }

        public List<Book> GetAllBooks() => _context.Books.ToList();

        public BookWithAuthorsVM GetBookById(int id)
        {
            var _bookWithAuthors = _context.Books.Where(n => n.Id == id).Select(bookVM => new BookWithAuthorsVM()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                isRead = bookVM.isRead,
                DateRead = bookVM.isRead ? bookVM.DateRead.Value : null,
                Rate = bookVM.isRead ? bookVM.Rate.Value : null,
                Genre = bookVM.Genre,
                CoverUrl = bookVM.CoverUrl,
                PublisherName = bookVM.Publisher.Name,
                AuthorNames = bookVM.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return _bookWithAuthors;
    }

        public Book UpdateBookById(int id, BookVM book) 
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == id);
            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.isRead = book.isRead;
                _book.DateRead = book.isRead ? book.DateRead.Value : null;
                _book.Rate = book.isRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;

                _context.SaveChanges();
            }
            return _book;
        }

        public void DeleteBookById(int id)
        {
            var _book = _context.Books.Find(id);
            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
    }
}
