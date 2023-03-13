using KondiProject.API.Models.Domains;

namespace KondiProject.API.Models.Dtos.Responses
{
    public class GymMachineResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? ImageName { get; set; }

        public List<GymMaschineInformation>? Informations { get; set; }
    }
}
