using Lib.Entities;
using System.ComponentModel.DataAnnotations;

namespace Lib.Models;

public class CreateBookRequest
{
    [Required]
    public string Title { get; set; }
    public string? ISBN { get; set; }
    [Required]
    public int RealeaseYear { get; set; }
}
