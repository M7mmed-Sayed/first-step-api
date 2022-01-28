using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModel;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorsService _authorSrevice;
        public AuthorsController(AuthorsService authorsService)
        {
            _authorSrevice=authorsService; ;
        }
        [HttpPost("Add-Author")]
        public IActionResult AddAuthor([FromBody]AuthorVM authorVM)
        {
            _authorSrevice.AddAuthor(authorVM); 
            return Ok();
        }
        [HttpGet("Get-Author-Books-by-id/{AuthorId}")]
        public IActionResult GetAuthorWithBooks(int AuthorId)
        {
            return Ok(_authorSrevice.GetAuthorWithBooks(AuthorId));
        }


    }
}
