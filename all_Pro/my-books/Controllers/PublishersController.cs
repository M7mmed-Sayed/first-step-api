using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModel;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;
        private readonly ILogger<PublishersController> _looger;
        public PublishersController(PublishersService publishersService, ILogger<PublishersController> looger)
        {
            _publishersService = publishersService; 
            _looger=looger;
        }
        [HttpPost("Add-Publisher")]
        public IActionResult AddPublisher([FromBody]PublisherVM publisherVM)
        {
            try
            {
                _publishersService.AddPublisher(publisherVM);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
            
        }
        [HttpGet("Publisher-Books-And-Authors-with-id/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            return Ok(_publishersService.GetPublisherData(id));
        }
        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherWithId(int id)
        {
            var res=_publishersService.GetPublisherWithId(id);
            if (res != null)
            {
                return Ok(res);
            }
            else return NotFound();
        }
        [HttpGet("Get-all-publisher")]
        public IActionResult GetAllPublisher(string sortBy, string searchString,int pageNumber)
        {
           // throw new Exception("this is throw from GetAllPublisher()");

            try
            {
                _looger.LogInformation("this is logger to GetAllPublisher()");
                return Ok(_publishersService.GetAllPublisher(sortBy, searchString, pageNumber));
            }
            catch (Exception )
            {

                return BadRequest("We can Load the Publisher");
            }
        }
        [HttpDelete("Delet-publisher-by-id/{id}")]
        public IActionResult DeletePublisher(int id)
        {
            try
            {
            _publishersService.DeletePublisherWithId(id);
            return Ok();

            }
            catch (Exception ex)
            {

                return NotFound();
            }
        }


    }
}
