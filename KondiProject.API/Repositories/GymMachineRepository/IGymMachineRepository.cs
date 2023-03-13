using KondiProject.API.Models.Domains;

namespace KondiProject.API.Repositories.GymMachineRepository
{
    public interface IGymMachineRepository
    {
        Task CreateAsync(GymMachine machine);
        Task<List<GymMachine>> GetAllByGymIdAsync(int gymId);
        Task<GymMachine?> GetByIdAsync(int id);
        Task DeleteAsync(GymMachine machine);
    }
}
