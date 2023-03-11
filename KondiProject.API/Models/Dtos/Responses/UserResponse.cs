using KondiProject.API.Enums;

namespace KondiProject.API.Models.Dtos.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }

        public Role Role { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
