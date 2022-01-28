using my_books.Data.Models;

namespace my_books.Data.Services
{
    public class LogsService
    {
        private AppDbContext _context;
        public LogsService(AppDbContext appDbContext)
        {
            _context=appDbContext;
        }
        public List<Log> GetAllLogsFromDB() => _context.Logs.ToList();
    }
}
