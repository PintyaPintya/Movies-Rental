using System.ComponentModel.DataAnnotations;

namespace MoviesRental.Models.Dto;

public class AddUserDto
{
    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string EmailAddress { get; set; }

    [Required]
    [StringLength(255)]
    public required string Password { get; set; }

    [Display(Name = "Date of Birth")]
    public DateOnly? BirthDate { get; set; }
}
