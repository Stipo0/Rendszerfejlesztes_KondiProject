using System.ComponentModel.DataAnnotations;

namespace KondiProject.API.Models.Dtos.Requests
{
    public class UserRegisterRequest
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
