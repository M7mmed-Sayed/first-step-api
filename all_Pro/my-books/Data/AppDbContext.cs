using Microsoft.EntityFrameworkCore;
using my_books.Data.Models;

namespace my_books.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)        
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>().HasOne(b=>b.Books)
                .WithMany(ba=>ba.Book_Authors)
                .HasForeignKey(bi=>bi.BooksId);
            modelBuilder.Entity<Book_Author>().HasOne(a => a.Author)
               .WithMany(ba => ba.Book_Authors)
               .HasForeignKey(a_i => a_i.AuthorId);
        }
        public DbSet<Books> Books { get; set; } 
        public DbSet<Publisher> publishers { get; set; } 
        public DbSet<Author>   Authors { get; set; } 
        public DbSet<Book_Author>   book_Authors { get; set; } 
        public DbSet<LogL>   LogsL { get; set; } 
        public DbSet<Log>   Logs { get; set; } 
        public DbSet<LogU>   LogsU { get; set; } 

    }
}
