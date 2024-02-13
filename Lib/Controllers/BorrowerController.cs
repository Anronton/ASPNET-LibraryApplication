using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lib.Entities;
using Lib.Models;

namespace Lib.Controllers
{
    
    [Route("[controller]")] 
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly LibDbContext _context;

        public BorrowerController(LibDbContext context)
        {
            _context = context;
        }

        [HttpPost] //skapa en låntagare ("Borrower")
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

        [HttpDelete("{id}")] //ta bort en låntagare ("Borrower/{id}")
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBorrower(int id)
        {
            var borrower = await _context.Borrowers.FindAsync(id);

            if (borrower == null)
            {
                return NotFound(new { Message = "Borrower was not found." });
            }

            _context.Borrowers.Remove(borrower);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
   
}