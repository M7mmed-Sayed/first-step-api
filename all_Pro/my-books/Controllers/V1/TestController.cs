using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace my_books.Controllers.V1
{
   // [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data-a")]
        public IActionResult Get()
        {
            return Ok("This is TestConroller V1");
        }
    }
}
