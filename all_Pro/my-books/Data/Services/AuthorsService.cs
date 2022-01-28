using my_books.Data.Models;
using my_books.Data.ViewModel;

namespace my_books.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }
        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
        }
        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Authors.Where(a => a.Id == authorId)
                .Select(author => new AuthorWithBooksVM()
                {
                    FullName = author.FullName,
                    BookTitles = author.Book_Authors.Select(t => t.Books.Title).ToList()
                }).FirstOrDefault();
            return _author;
        }
    }
}
