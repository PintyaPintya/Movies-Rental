using MoviesRental.Models;

namespace MoviesRental.IRepository;

public interface IUserRepository
{
    Task<bool> CheckIfEmailExists(string email);
    Task Add(User user);
    Task<List<User>> GetAllUsers();
    Task<User?> GetById(int id);
    Task Update(User user);
    Task Delete(User user);
}
