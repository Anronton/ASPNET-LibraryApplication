using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib.Entities;

public class Loan
{
    public int Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    [ForeignKey("BookId")]
    public int BookId { get; set; }
    [ForeignKey("BorrowerId")]
    public int BorrowerId {  get; set; }

    [Required]
    public Book Book { get; set; }
    [Required]
    public Borrower Borrower { get; set; }
}
