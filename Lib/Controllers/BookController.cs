using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lib.Entities;
using Lib.Models;

namespace Lib.Controllers
{
    
    [Route("[controller]")] 
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibDbContext _context;

        public BookController(LibDbContext context)
        {
            _context = context;
        }

        [HttpPost] // skapa en bok ("Book")
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostBook(CreateBookRequest bookRequest)
        {
            try
            {
                var book = new Book
                {
                    Title = bookRequest.Title,
                    ISBN = bookRequest.ISBN,
                    RealeaseYear = bookRequest.RealeaseYear,
                };

                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                return StatusCode(201, new { Message = $"Book was successfully created with title: '{book.Title}', id: {book.Id}, and was released: {book.RealeaseYear}." });
            }
            catch
            {
                return BadRequest(new { Message = "An error occurred while processing the request." });
            }
        }

        [HttpDelete("{id}")] // ta bort en bok ("Book/{id}")
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound(new { Message = "Book was not found." });
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]  //hämta alla böcker ("Books")
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<BookDto>> GetBooks()
        {
            return await _context.Books.AsNoTracking()
                .Select(b => new BookDto()
                {
                    Id = b.Id,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    RealeaseYear = b.RealeaseYear,
                })
                .ToListAsync();
        }
    
        [HttpGet("{id}")] // hämtar en specifik bok ("Book/{id}")
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpecificBookDto>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound(new { Message = "Book was not found." });
            }

            var specificBookDto = new SpecificBookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                RealeaseYear = book.RealeaseYear,
                IsAvailable = book.IsAvailable,
            };

            return specificBookDto;
        }

    }
    
}
