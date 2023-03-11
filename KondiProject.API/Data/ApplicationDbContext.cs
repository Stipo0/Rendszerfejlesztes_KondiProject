using KondiProject.API.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace KondiProject.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) {}

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Gym> Gyms { get; set; } = null!;
        public DbSet<GymMachine> GymMachines { get; set; } = null!;
        public DbSet<GymMaschineInformation> GymMaschineInformations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
