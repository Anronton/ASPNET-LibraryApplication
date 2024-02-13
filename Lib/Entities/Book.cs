using System.ComponentModel.DataAnnotations.Schema;

namespace Lib.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ISBN { get; set; }
    public int RealeaseYear { get; set; }
    public bool IsAvailable { get; set; } = true;

    public ICollection<Loan> Loans { get; set; }
}
