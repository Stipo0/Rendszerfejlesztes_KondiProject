using KondiProject.API.Enums;

namespace KondiProject.API.Models.Domains
{
    public class Gym
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public List<GymMachine>? GymMachines { get; set; }
    }
}
