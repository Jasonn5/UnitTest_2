using Library.Controllers;
using Library.Data.Models;
using Library.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Library.Test
{
    public class BooksControllerTest
    {
        BooksController _booksController;
        IBookService _bookService;

        public BooksControllerTest() 
        {
            _bookService= new BookService();
            _booksController = new BooksController(_bookService);
        }

        [Fact]
        public void GetAllTest() 
        {
            //arrange

            //act
            var result = _booksController.Get();
            //assert
            Assert.IsType<OkObjectResult>(result.Result);
            var list = result.Result as OkObjectResult;

            Assert.IsType<List<Book>>(list.Value);
            var listBooks = list.Value as List<Book>;

            Assert.Equal(5, listBooks.Count);
        }

        [Theory]
        [InlineData("1b4076b1-fe31-4161-af58-6b3f69d93e55", "1b4076b1-fe31-4161-af58-6b3f69d93011")]
        public void GetBookByIdTest(string guid1, string guid2) 
        {
            //arrange
            var validGuid = new Guid(guid1);
            var invalidGuid = new Guid(guid2);
            //act
            var notFoundresult = _booksController.Get(invalidGuid);
            var okResult = _booksController.Get(validGuid);
            //assert
            Assert.IsType<NotFoundResult>(notFoundresult.Result);

            Assert.IsType<OkObjectResult>(okResult.Result);

            var book  = okResult.Result as OkObjectResult;
            Assert.IsType<Book>(book.Value);

            var bookItem = book.Value as Book;
            Assert.Equal(validGuid, bookItem.Id);
            Assert.Equal("Mananing Onselft", bookItem.Title);
        }

        [Fact]
        public void AddBookTest() 
        {
            //arrange
            var completeBook = new Book() 
            {
                Id= Guid.NewGuid(),
                Author = "Author",
                Title = "Title",
                Description= "Description",
            };
            //act
            var createdResponse = _booksController.Post(completeBook);
            //assert
            Assert.IsType<CreatedResult>(createdResponse.Result);

            var book = createdResponse.Result as CreatedResult;
            Assert.IsType<Book>(book.Value);

            var bookItem = book.Value as Book;
            Assert.Equal(completeBook.Author, bookItem.Author);
            Assert.Equal(completeBook.Title, bookItem.Title);
            Assert.Equal(completeBook.Description, bookItem.Description);

            //arrange
            var inCompleteBook = new Book()
            {
                Author = "Author",
                Description = "Description",
            };
            //act
            _booksController.ModelState.AddModelError("Tittle", "Title is a required field");
            var badResponse = _booksController.Post(inCompleteBook);
            //assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        [Theory]
        [InlineData("1b4076b1-fe31-4161-af58-6b3f69d93e55", "1b4076b1-fe31-4161-af58-6b3f69d93011")]
        public void RemoveBookByIdTest(string guid1, string guid2)
        {
            //arrange
            var validGuid = new Guid(guid1);
            var invalidGuid = new Guid(guid2);
            //act
            var notFoundresult = _booksController.Delete(invalidGuid);
            //assert
            Assert.IsType<NotFoundResult>(notFoundresult);
            Assert.Equal(5, _bookService.GetAll().Count());

            //act
            var okResult = _booksController.Delete(validGuid);
            //assert
            Assert.IsType<OkResult>(okResult);
            Assert.Equal(4, _bookService.GetAll().Count());
        }
    }
}