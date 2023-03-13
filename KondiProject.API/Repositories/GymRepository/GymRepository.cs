using KondiProject.API.Data;
using KondiProject.API.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace KondiProject.API.Repositories.GymRepository
{
    public class GymRepository : IGymRepository
    {
        private readonly ApplicationDbContext _context;

        public GymRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Gym gym)
        {
            await _context.AddAsync(gym);
            await _context.SaveChangesAsync();
        }

        public Task<Gym?> GetByIdAsync(int id)
        {
            return _context.Gyms.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
        }

        public Task<Gym?> GetByEmailAsync(string email)
        {
            return _context.Gyms.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<List<Gym>> GetAllAsync()
        {
            return _context.Gyms.AsNoTracking().ToListAsync();
        }

        public Task<bool> EmailIsExistAsync(string email)
        {
            return _context.Gyms.AsNoTracking().AnyAsync(x => x.Email == email);
        }

        public Task<bool> NameIsExistAsync(string name)
        {
            return _context.Gyms.AsNoTracking().AnyAsync(x => x.Name == name);
        }

        public Task<bool> PhoneNumberIsExistAsync(string phoneNumber) 
        {
            return _context.Gyms.AsNoTracking().AnyAsync(x=> x.PhoneNumber == phoneNumber);
        }

        public Task DeleteAsync(Gym gym) 
        {
            _context.Gyms.Remove(gym);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
