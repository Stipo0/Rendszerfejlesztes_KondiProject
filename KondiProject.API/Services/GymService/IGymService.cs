using KondiProject.API.Models.Domains;
using KondiProject.API.Models.Dtos.Requests;

namespace KondiProject.API.Services.GymService
{
    public interface IGymService
    {
        Task RegisterAsync(CreateGymRequest reguest);
        Task<Gym?> GetByIdAsync(int id);
        Task<Gym?> GetByEmailAsync(string email);
        Task<List<Gym>?> GetAllAsync();
        Task<bool> EmailIsExistAsync(string email);
        Task<bool> NameIsExistAsync(string name);
        Task<bool> PhoneNumberIsExistAsync(string phoneNumber);
        Task DeleteAsync(Gym gym);
        bool VerifyLogin(Gym gym, string password);
        string CreateToken(Gym gym);
    }
}