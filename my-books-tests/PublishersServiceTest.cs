using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Exceptions;
using my_books.Data.ViewModel;
using my_books.Data.Models;
using my_books.Data.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace my_books_tests
{
    public class PublishersServiceTest
    {
        private static DbContextOptions<AppDbContext> DpContextOptoins =
            new DbContextOptionsBuilder<AppDbContext>().
            UseInMemoryDatabase(databaseName: "BookDBTest")
            .Options;
        private AppDbContext context;
        PublishersService publishersService;
        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(DpContextOptoins);
            context.Database.EnsureCreated();

            seedDatabes();

            publishersService = new PublishersService(context);
        }
        [Test,Order(1)]
        public void GetAllPublisher_NSort_NSearch_NPage()
        {
            var resualt = publishersService.GetAllPublisher("", "", null);
            Assert.That(resualt.Count, Is.EqualTo(5));
        }
        [Test, Order(2)]
        public void GetAllPublisher_NSort__NSearch_Page()
        {
            var resualt = publishersService.GetAllPublisher("", "", 2);
            Assert.That(resualt.Count, Is.EqualTo(1));
        }
        [Test , Order(3)]
        public void GetAllPublisher_NSort_Search_NPage()
        {
            var resualt = publishersService.GetAllPublisher("", "3", null);
            Assert.That(resualt.Count, Is.EqualTo(1));
            Assert.That(resualt.FirstOrDefault().Name, Is.EqualTo("publisher 3"));
        }
        [Test, Order(4)]
        public void GetAllPublisher_Sort_NSearch_NPage()
        {
            var resualt = publishersService.GetAllPublisher("name_desc", "", null);
            Assert.That(resualt.Count, Is.EqualTo(5));
            Assert.That(resualt.FirstOrDefault().Name, Is.EqualTo("publisher 6"));
        }
        [Test, Order(4)]
        public void GetPublisher_By_ID()
        {
            var resualt = publishersService.GetPublisherWithId(1);
            Assert.That(resualt.Name, Is.EqualTo("publisher 1"));
        }
      /*  [Test,Order(5)]
        public void AddPublisher_With_Ex()
        {
            var publisher = new PublisherVM()
            {
                Name = "7 publisher test 7"
            };
            publishersService.AddPublisher(publisher);
            Assert.That(() => publishersService.AddPublisher(publisher), 
                Throws.Exception.TypeOf<Exception>()
                .With.Message.EqualTo("NUMP"));
        }*/

        [Test, Order(6)]
        public void AddPublisher_WithOut_Ex()
        {
            var publisher = new PublisherVM()
            {
                Name = "publisher test 8"
            };
            publishersService.AddPublisher(publisher);
        }
        [Test, Order(7)]
        public void GetPublisherData_Test()
        {
            var resualt = publishersService.GetPublisherData(1);
            Assert.That(resualt.Name, Is.EqualTo("publisher 1"));
            Assert.That(resualt.bookAuthors, Is.Not.Empty);
            Assert.That(resualt.bookAuthors.Count, Is.GreaterThan(0));
            Assert.That(resualt.bookAuthors.OrderBy(n=>n.BookName).FirstOrDefault().BookName, Is.EqualTo("book 1"));
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();

        }
        private void seedDatabes()
        {
            var Publishers = new List<Publisher>()
            {
                new Publisher()
                {
                    Id=1,
                    Name="publisher 1"
                },
                 new Publisher()
                {
                    Id=2,
                    Name="publisher 2"
                },
                  new Publisher()
                {
                    Id=3,
                    Name="publisher 3"
                },
                  new Publisher()
                {
                    Id=4,
                    Name="publisher 4"
                },
                 new Publisher()
                {
                    Id=5,
                    Name="publisher 5"
                },
                  new Publisher()
                {
                    Id=6,
                    Name="publisher 6"
                }
            };
            context.publishers.AddRange(Publishers);

            var authors = new List<Author>()
            {
                new Author()
                {
                    Id=1,
                    FullName="author 1"
                },
                new Author()
                {
                    Id=2,
                    FullName="author 2"
                },
                new Author()
                {
                    Id=3,
                    FullName="author 3"
                }
            };
            context.Authors.AddRange(authors);

            var books = new List<Books>() {
                new Books()
                {
                    Id=1,
                    Title="book 1",
                    Description ="B1 desc",
                    IsRead=false,
                    Genre="gn1",
                    CoverUrl="http://adda",
                    DateAdded=DateTime.Now.AddDays(-10),
                    PublisherId=1
                }
                , new Books()
                {
                    Id=2,
                    Title="book 2",
                    Description ="B2 desc",
                    IsRead=false,
                    Genre="gn2",
                    CoverUrl="http://adda2",
                    DateAdded=DateTime.Now.AddDays(-10),
                    PublisherId=1
                }
            };
            context.Books.AddRange(books);

            var Books_Autours = new List<Book_Author>()
            {
                new Book_Author()
                {
                    Id=1,
                    BooksId=1,
                    AuthorId=1
                },
                new Book_Author()
                {
                    Id=2,
                    BooksId=1,
                    AuthorId=2
                },
                new Book_Author()
                {
                    Id=3,
                    BooksId=2,
                    AuthorId=2
                }
            };
            context.book_Authors.AddRange(Books_Autours);

            context.SaveChanges();

        }



    }
}