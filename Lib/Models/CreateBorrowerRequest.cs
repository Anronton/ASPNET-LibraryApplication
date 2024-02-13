using Lib.Entities;
using System.ComponentModel.DataAnnotations;

namespace Lib.Models;

public class CreateBorrowerRequest
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [RegularExpression(@"^\d{6}-\d{4}$", ErrorMessage = "Social Security Number must have the format YYMMDD-XXXX.")]
    public string SocialSecurityNumber { get; set; }
}
