using KondiProject.API.Enums;

namespace KondiProject.API.Models.Domains
{
    public class User
    {
        public int Id { get; set; }
        public Role Role { get; set; } = Role.User;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;
    }
}
