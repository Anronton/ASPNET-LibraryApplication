

namespace Lib.Entities;

public class Borrower
{
    public int Id { get; set; } 
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string SocialSecurityNumber { get; set; }

    public ICollection<Loan> Loans { get; set; }
}
