using my_books.Data.Models;
using my_books.Data.Paging;
using my_books.Data.ViewModel;
using System.Text.RegularExpressions;
using my_books.Exceptions;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }
        public void AddPublisher(PublisherVM publisher)
        {
            if (StringStartWithNumber(publisher.Name))
                throw new Exception("NUMP");
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.publishers.Add(_publisher);
            _context.SaveChanges();
        }
        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisher = _context.publishers.Where(p => p.Id == publisherId)
                .Select(publisher => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = publisher.Name,
                    bookAuthors = publisher.Books.Select(PB => new BookAuthorVM()
                    {
                        BookName = PB.Title,
                        BookAuthors = PB.Book_Authors.Select(A => A.Author.FullName).ToList()

                    }).ToList()
                }).FirstOrDefault();
            return _publisher;
        }
        public void DeletePublisherWithId(int publisherId)
        {
            var _publisher = _context.publishers.FirstOrDefault(P => P.Id == publisherId);
            if (_publisher != null)
            {
                _context.publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else throw new PublisherNameException("LOL");
        }
        public Publisher GetPublisherWithId(int publisherId) => _context.publishers.FirstOrDefault(P => P.Id == publisherId);

        public List<Publisher> GetAllPublisher(string sortBy, string searchString, int? pageNumber)
        {
            var allpublisher = _context.publishers.OrderBy(n => n.Name).ToList();
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allpublisher = _context.publishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                allpublisher = allpublisher.Where(P => P.Name.
                Contains(searchString,StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            //paging 
            int pagesize = 5;
            allpublisher = PaginatedList<Publisher>.Create(allpublisher.AsQueryable(), pageNumber ?? 1, pagesize);
            return allpublisher;
        }
        private bool StringStartWithNumber(string name) => (Regex.IsMatch(name, @"^\d"));
    }
}
