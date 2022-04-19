using BookStore.API.Models;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute]int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody]BookModel bookModel )
        {
            var id = await _bookRepository.AddBookAsync(bookModel);

            return CreatedAtAction(nameof(GetBookById),new {id = id,Controller = "books" },id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook ([FromBody] BookModel bookModel,[FromRoute]int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            await _bookRepository.UpdateBookAsync(id,bookModel);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book==null)
            {
                return NotFound();
            }
            await _bookRepository.DeleteBookAsync(id);
            
            return Ok();
        }
    }
}
