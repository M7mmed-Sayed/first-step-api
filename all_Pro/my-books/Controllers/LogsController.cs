using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private LogsService _logsServises;
        public LogsController(LogsService logsServises)
        {
            _logsServises = logsServises;
        }
        [HttpGet("get-all-logs")]
        public IActionResult GetAllLogsFromDB()
        {
            try
            {
                return Ok(_logsServises.GetAllLogsFromDB());
            }
            catch (Exception ex)
            {
                return BadRequest("could not load logs from data bases"+ex.Message);
            }
        }
    }
}
