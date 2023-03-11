using KondiProject.API.Data;
using KondiProject.API.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace KondiProject.API.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public Task<User?> GetByIdAsync(int id) 
        {
            return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _context.Users.AsNoTracking().FirstOrDefaultAsync(u =>u.Email == email);
        }

        public Task<List<User>> GetAllAsync() 
        { 
            return _context.Users.AsNoTracking().ToListAsync();
        }

        public Task<bool> EmailIsExistAsync(string email)
        {
            return _context.Users.AsNoTracking().AnyAsync(x => x.Email == email);
        }

        public Task<bool> UsernameIsExistAsync(string username)
        {
            return _context.Users.AsNoTracking().AnyAsync(x => x.Username == username);
        }

        public Task DeleteUserAsync(User user) 
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
