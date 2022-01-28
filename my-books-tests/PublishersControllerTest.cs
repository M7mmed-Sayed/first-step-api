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
using my_books.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace my_books_tests
{
    internal class PublishersControllerTest
    {
        private static DbContextOptions<AppDbContext> DpContextOptoins =
           new DbContextOptionsBuilder<AppDbContext>().
           UseInMemoryDatabase(databaseName: "BookDBTest")
           .Options;
        private AppDbContext context;
        PublishersService publishersService;
        PublishersController publishersController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(DpContextOptoins);
            context.Database.EnsureCreated();

            Seed();
            publishersService = new PublishersService(context);
            publishersController = new PublishersController(publishersService, new NullLogger<PublishersController>());
        }
        [Test ,Order(1)]
        public void HTTP_GET_GetAllPublisherSort_Search_Page()
        {
            IActionResult actionFirst = publishersController
                .GetAllPublisher("name_desc", "publisher", 1);
            Assert.That(actionFirst,Is.TypeOf<OkObjectResult>());
            var actionFirstData = (actionFirst as OkObjectResult).Value as List<Publisher>;
            Assert.That(actionFirstData.First().Name, Is.EqualTo("Publisher 6").IgnoreCase);
            Assert.That(actionFirstData.First().Id, Is.EqualTo(6));
            Assert.That(actionFirstData.Count, Is.EqualTo(5));

        }
        [Test,Order(2)]
        public void HTTP_GET_GetPublisherWithIdOk()
        {
            IActionResult action = publishersController.GetPublisherWithId(1);
            Assert.That(action, Is.TypeOf<OkObjectResult>());
            var actionData = (action as OkObjectResult).Value as Publisher;
            Assert.That(actionData.Name, Is.EqualTo("Publisher 1").IgnoreCase);

        }
        [Test, Order(3)]
        public void HTTP_GET_GetPublisherWithIdNotOk()
        {
            IActionResult action = publishersController.GetPublisherWithId(99);
            Assert.That(action, Is.TypeOf<NotFoundResult>());

        }
        [Test,Order(4)]
        public void HTTP_POST_AddPublisherOk()
        {
            var _publisher = new PublisherVM()
            {
                Name = "Test Post"
            };
            IActionResult action = publishersController.AddPublisher(_publisher);
            Assert.That(action, Is.TypeOf<OkResult>());

        }
        [Test, Order(5)]
        public void HTTP_POST_AddPublisherBad()
        {
            var _publisher = new PublisherVM()
            {
                Name = "123Test Post"
            };
            IActionResult action = publishersController.AddPublisher(_publisher);
            Assert.That(action, Is.TypeOf<BadRequestObjectResult>());

        }
        [Test, Order(5)]
        public void HTTP_Delet_DeletPublisherbyId()
        {
           
            IActionResult action = publishersController.DeletePublisher(100);
            Assert.That(action, Is.TypeOf<NotFoundResult>());

        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();

        }
        private void Seed()
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
