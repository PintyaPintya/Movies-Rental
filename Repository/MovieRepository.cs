using Microsoft.EntityFrameworkCore;
using MoviesRental.Data;
using MoviesRental.IRepository;
using MoviesRental.Models;

namespace MoviesRental.Repository;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Movie>> GetAll()
    {
        try
        {
            return await _context.Movies.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }

    public async Task<bool> CheckIfExists(string name)
    {
        try
        {
            return await _context.Movies.AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }

    public async Task Add(Movie movie)
    {
        try
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }

    public async Task<Movie?> GetById(int id)
    {
        try
        {
            return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }

    public async Task Update(Movie movie)
    {
        try
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }

    public async Task Delete(Movie movie)
    {
        try
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }
}