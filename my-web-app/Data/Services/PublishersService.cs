using my_web_app.Data.Models;
using my_web_app.Data.ViewModels;
using my_web_app.Exceptions;
using System.Text.RegularExpressions;

namespace my_web_app.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisherVM)
        {

            if (StringStartsWithNumber(publisherVM.Name)) throw new PublisherNameException("Name starts with number", publisherVM.Name);
            var _publisher = new Publisher()
            {
                Name = publisherVM.Name,
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;

        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n => n.Id == id);

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int id)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == id)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher); 
                _context.SaveChanges();
            } else
            {
                throw new Exception($"The publisher with the id: {id} does not exist");
            }
        }

        private bool StringStartsWithNumber(string name) => (Regex.IsMatch(name, @"^\d"));
    }
}
