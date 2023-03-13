using KondiProject.API.Models.Domains;

namespace KondiProject.API.Repositories.GymRepository
{
    public interface IGymRepository
    {
        Task CreateAsync(Gym gym);

        Task<Gym?> GetByIdAsync(int id);

        Task<Gym?> GetByEmailAsync(string email);

        Task<List<Gym>> GetAllAsync();

        Task<bool> EmailIsExistAsync(string email);

        Task<bool> NameIsExistAsync(string name);

        Task<bool> PhoneNumberIsExistAsync(string phoneNumber);

        Task DeleteAsync(Gym gym);
    }
}
