using KondiProject.API.Models.Domains;
using KondiProject.API.Models.Dtos.Requests;

namespace KondiProject.API.Services.GymMachineService
{
    public interface IGymMachineService
    {
        Task CreateMachineAsync(int gymId, CreateGymMachineRequest request);
        Task<List<GymMachine>> GetAllByGymIdAsync(int gymId);
        Task<GymMachine?> GetByIdAsync(int id);
        Task DeleteAsync(GymMachine machine);
    }
}
