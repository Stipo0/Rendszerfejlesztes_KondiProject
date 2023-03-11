using System.ComponentModel.DataAnnotations;

namespace KondiProject.API.Models.Dtos.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
