using Microsoft.EntityFrameworkCore;
using MoviesRental.Models;

namespace MoviesRental.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}

    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserMovie> UserMovies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>().HasData(
            new Movie
            {
                Id = 1,
                Name = "The Shawshank Redemption",
                Genre = "Drama",
                ReleaseDate = new DateOnly(1994, 9, 22),
                DateAdded = new DateTime(2025, 1, 13),
                Stock = 3
            },
            new Movie
            {
                Id = 2,
                Name = "The Godfather",
                Genre = "Crime",
                ReleaseDate = new DateOnly(1972, 3, 24),
                DateAdded = new DateTime(2025, 1, 13),
                Stock = 5
            },
            new Movie
            {
                Id = 3,
                Name = "The Dark Knight",
                Genre = "Action",
                ReleaseDate = new DateOnly(2008, 7, 18),
                DateAdded = new DateTime(2025, 1, 13),
                Stock = 5
            },
            new Movie
            {
                Id = 4,
                Name = "Forrest Gump",
                Genre = "Drama",
                ReleaseDate = new DateOnly(1994, 7, 6),
                DateAdded = new DateTime(2025, 1, 13),
                Stock = 5
            },
            new Movie
            {
                Id = 5,
                Name = "Inception",
                Genre = "Sci-Fi",
                ReleaseDate = new DateOnly(2010, 7, 16),
                DateAdded = new DateTime(2025, 1, 13),
                Stock = 5
            }
        );

    }
}