using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lib.Entities;
using Lib.Models;

namespace Lib.Controllers
{
    
    [Route("[controller]")] 
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly LibDbContext _context;

        public LoanController(LibDbContext context)
        {
            _context = context;
        }

        [HttpPost("{bookId}/{borrowerId}")] // göra ett lån ("Loan/{bookId}/{borrowerId}")
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

        [HttpPatch("{loanId}")] //uppdaterar för att lämna tillbaka boken ("ReturnLoan/{loanId}")
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
    
}