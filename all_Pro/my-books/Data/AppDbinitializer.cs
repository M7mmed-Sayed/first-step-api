using my_books.Data.Models;
namespace my_books.Data
{
    public class AppDbinitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        { 
            using (var servicescope=applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = servicescope.ServiceProvider.GetService<AppDbContext>();
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Books()
                    {
                        Title = "1st Book title",
                        Description = "1st Book description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre="Biography",
                        CoverUrl="Https....",
                        DateAdded=DateTime.Now
                    }, new Books()
                    {
                        Title = "2nd Book title",
                        Description = "2ed Book description",
                        IsRead = false,
                        Genre = "Biography",
                        CoverUrl = "Https....",
                        DateAdded = DateTime.Now
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
