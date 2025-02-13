using Microsoft.EntityFrameworkCore;
using MoviesRental.Data;
using MoviesRental.IRepository;
using MoviesRental.Models;

namespace MoviesRental.Repository;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IUserRepository _userRepository;

    public MovieRepository(ApplicationDbContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
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

    public async Task<bool> Rent(int movieId, int userId)
    {
        try
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userRepository.GetById(userId);
                if (user == null) return false;

                var movie = await GetById(movieId);
                if (movie == null) return false;

                var userMovie = new UserMovie()
                {
                    MovieId = movieId,
                    UserId = user.Id
                };
                user.Movies.Add(userMovie);
                _context.Users.Update(user);

                await _context.UserMovies.AddAsync(userMovie);

                movie.Stock -= 1;
                _context.Movies.Update(movie);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }
}