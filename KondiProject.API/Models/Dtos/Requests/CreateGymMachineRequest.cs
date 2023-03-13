using System.ComponentModel.DataAnnotations;

namespace KondiProject.API.Models.Dtos.Requests
{
    public class CreateGymMachineRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        public IFormFile? Image { get; set; }
    }
}
