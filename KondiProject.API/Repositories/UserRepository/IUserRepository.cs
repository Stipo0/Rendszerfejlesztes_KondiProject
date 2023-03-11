using KondiProject.API.Models.Domains;

namespace KondiProject.API.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<List<User>> GetAllAsync();
        Task<bool> EmailIsExistAsync(string email);
        Task<bool> UsernameIsExistAsync(string username);
        Task DeleteUserAsync(User user);
    }
}
