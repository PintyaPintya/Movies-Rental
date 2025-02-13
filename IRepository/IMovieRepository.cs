using MoviesRental.Models;

namespace MoviesRental.IRepository;

public interface IMovieRepository
{
    Task<List<Movie>> GetAll();
    Task<bool> CheckIfExists(string name);
    Task Add(Movie movie);
    Task<Movie?> GetById(int id);
    Task Update(Movie movie);
    Task Delete(Movie movie);
    Task<bool> Rent(int movieId, int userId);
}