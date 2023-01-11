using Library.Data.Models;
using Library.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var books = _bookService.GetAll();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(Guid id)
        {
            var book = _bookService.GetById(id);

            if (book == null) 
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book book)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var newBook = _bookService.Add(book);

            return Created("api/books", newBook);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id) 
        {
            var existingBook = _bookService.GetById(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            _bookService.Remove(id);

            return Ok();
        }
    }
}
