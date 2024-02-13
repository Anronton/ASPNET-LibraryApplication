namespace Lib.Models;

public class SpecificBookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ISBN { get; set; }
    public int RealeaseYear { get; set; }
    public bool IsAvailable { get; set; }
}