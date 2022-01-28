using my_books.Data.Models;
using my_books.Data.ViewModel;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }
        public void AddBookWithAuthor(BookVM book)
        {
            var _book = new Books()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };
            _context.Books.Add(_book);
            _context.SaveChanges();
            foreach (var Id in book.AuhorIds)
            {
                var _book_author = new Book_Author()
                {
                    BooksId = _book.Id,
                    AuthorId = Id
                };
                _context.book_Authors.Add(_book_author);
                _context.SaveChanges();
            }
        }
        public List<Books> GetAllBooks() => _context.Books.ToList();
        public BookWithAuthorVM GetBookById(int bookId)
        {
            var bookwihauthors = _context.Books.Where(b => b.Id == bookId)
                .Select(book => new BookWithAuthorVM()
                {
                    Title = book.Title,
                    Description = book.Description,
                    IsRead = book.IsRead,
                    DateRead = book.IsRead ? book.DateRead.Value : null,
                    Rate = book.IsRead ? book.Rate : null,
                    Genre = book.Genre,
                    CoverUrl = book.CoverUrl,
                    PublisherName = book.Publisher.Name,
                    AuthorName = book.Book_Authors.Select(n => n.Author.FullName).ToList()
                }).FirstOrDefault();
            return bookwihauthors;
        }
        public Books UpdateBookById(int bookId, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(_Book => _Book.Id == bookId);
            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;
                _context.SaveChanges();
            }
            return _book;
        }
        public void DeleteBookById(int bookId)
        {
            var _book = _context.Books.FirstOrDefault(_Book => _Book.Id == bookId);
            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }

    }
}
