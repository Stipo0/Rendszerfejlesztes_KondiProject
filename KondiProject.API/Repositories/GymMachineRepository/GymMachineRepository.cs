using KondiProject.API.Data;
using KondiProject.API.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace KondiProject.API.Repositories.GymMachineRepository
{
    public class GymMachineRepository : IGymMachineRepository
    {
        private readonly ApplicationDbContext _context;

        public GymMachineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(GymMachine machine) 
        {
            await _context.AddAsync(machine);
            await _context.SaveChangesAsync();
        }

        public Task<List<GymMachine>> GetAllByGymIdAsync(int gymId) 
        {
            return _context.GymMachines.AsNoTracking().Where(x => x.GymId == gymId).ToListAsync();
        }

        public Task<GymMachine?> GetByIdAsync(int id) 
        {
            return _context.GymMachines.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task DeleteAsync( GymMachine machine) 
        {
            _context.GymMachines.Remove(machine);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
