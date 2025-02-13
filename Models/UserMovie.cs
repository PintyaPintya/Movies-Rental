using System.ComponentModel.DataAnnotations;

namespace MoviesRental.Models;

public class UserMovie
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? user { get; set; }
    public int MovieId { get; set; }
    public Movie? Movie { get; set; }
}
