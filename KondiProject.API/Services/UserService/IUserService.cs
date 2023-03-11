using KondiProject.API.Models.Domains;
using KondiProject.API.Models.Dtos.Requests;
using KondiProject.API.Models.Dtos.Responses;

namespace KondiProject.API.Services.UserService
{
    public interface IUserService
    {
        Task RegisterAsync(UserRegisterRequest reguest);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        bool VerifyLogin(User user, string password);
        string CreateToken(User user);
        Task<List<User>?> GetAllAsync();
        Task<bool> EmailIsExistAsync(string email);
        Task<bool> UsernameIsExistAsync(string username);
        Task DeleteUserAsync(User user);
    }
}
