using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lib.Entities;
using Lib.Models;

namespace Lib.Controllers   // huvudkontrollern, vi plockar metoder härifrån!
{
    /*
    [Route("[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly LibDbContext _context;

        public LibraryController(LibDbContext context)
        {
            _context = context;
        }


        [HttpPost("Book")] // skapa en bok
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

        [HttpPost("Borrower")] //skapa en låntagare
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostBorrower(CreateBorrowerRequest borrowerRequest)
        {
            try
            {
                if (_context.Borrowers.Any(b => b.SocialSecurityNumber == borrowerRequest.SocialSecurityNumber))
                {
                    ModelState.AddModelError(nameof(CreateBorrowerRequest.SocialSecurityNumber), "Social Security Number must be unique.");
                    return BadRequest(ModelState);
                }

                var borrower = new Borrower
                {
                    FirstName = borrowerRequest.FirstName,
                    LastName = borrowerRequest.LastName,
                    SocialSecurityNumber = borrowerRequest.SocialSecurityNumber,
                };

                _context.Borrowers.Add(borrower);
                await _context.SaveChangesAsync();

                return StatusCode(201, new { Message = $"Borrower: '{borrower.FirstName} {borrower.LastName}' was successfully created with id: {borrower.Id}." });
            }
            catch
            {
                return BadRequest(new { Message = "An error occurred while processing the request." });
            }
        }

        
        [HttpDelete("Book/{id}")] // ta bort en bok
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound(new { Message = "Book was not found."});
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("Borrower/{id}")] //ta bort en låntagare
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBorrower(int id)
        {
            var borrower = await _context.Borrowers.FindAsync(id);

            if (borrower == null)
            {
                return NotFound(new { Message = "Borrower was not found."});
            }

            _context.Borrowers.Remove(borrower);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpGet("Books")]  //hämta alla böcker
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

        [HttpGet("Book/{id}")] // hämtar en specifik bok
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpecificBookDto>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound(new { Message = "Book was not found."});
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

        [HttpPost("Loan/{bookId}/{borrowerId}")] // göra ett lån
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostLoan(int bookId, int borrowerId)
        {
            var borrower = await _context.Borrowers.FindAsync(borrowerId);
            var book = await _context.Books.FindAsync(bookId);

            if (borrower == null || book == null)
            {
                return NotFound(new { Message = "Borrower/Book or neither was found." });
            }

            if (book.IsAvailable == false)
            {
                return BadRequest(new { Message = "Book is not available for loan." });
            }

            var loan = new Loan
            {
                BorrowerId = borrowerId,
                BookId = bookId,
                LoanDate = DateTime.Now,
            };

            book.IsAvailable = false;

            _context.Loans.Add(loan);

            await _context.SaveChangesAsync();

            return StatusCode(201, new { Message = $"Loan created sucessfully with id: {loan.Id}. {borrower.FirstName} {borrower.LastName}(borrower id: {borrower.Id}) has borrowed '{book.Title}'(book id: {book.Id})" });
        }


        [HttpPatch("ReturnLoan/{loanId}")] //uppdaterar för att lämna tillbaka boken
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> ReturnLoan(int loanId)
        {
            var loan = await _context.Loans.Include(l => l.Book).FirstOrDefaultAsync(l => l.Id == loanId);

            if (loan == null)
            {
                return NotFound(new { Message = "Loan was not found." });
            }

            if (loan.ReturnDate.HasValue)
            {
                return BadRequest(new { Message = "Loan has already been returned." });
            }

            loan.ReturnDate = DateTime.Now;
            loan.Book.IsAvailable = true;

            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Loan returned successfully. '{loan.Book.Title}' with id: {loan.Book.Id} is now avialable" });
        }
    }
    */
}
