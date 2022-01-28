using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModel;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BooksService _bookService;
        public BooksController(BooksService booksService)
        {
            _bookService  = booksService;
        }
        [HttpGet("Get-all-books")]
        public IActionResult GetAllBooks()
        {
            var allbooks = _bookService.GetAllBooks();
            return Ok(allbooks);
        }
        [HttpGet("Get-book-by-Id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            return Ok(book);
        }
        [HttpPost("add-book-with-Authors")]
        public IActionResult Addbook([FromBody]BookVM book)
        {
            _bookService.AddBookWithAuthor(book);
            return Ok();
        }
        [HttpPut("Edit-Book-By-Id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookVM book)
        {
            var UpdatedBook = _bookService.UpdateBookById(id, book);
            return Ok(UpdatedBook);
        }
        [HttpDelete("Delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
           _bookService.DeleteBookById(id);
            return Ok();
        }

    }
}
