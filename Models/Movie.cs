using System.ComponentModel.DataAnnotations;

namespace MoviesRental.Models;

public class Movie
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    [Required]
    [StringLength(255)]
    public required string Genre { get; set; }

    [Required]
    [Display(Name = "Release Date")]
    public DateOnly ReleaseDate { get; set; }

    [Required]
    [Display(Name = "Date Added")]
    public DateTime DateAdded { get; set; }

    [Required]
    [Range(1, 20, ErrorMessage = "Stock should be between 1 and 20")]
    public int Stock { get; set; }
}