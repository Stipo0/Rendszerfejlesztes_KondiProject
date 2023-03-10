namespace KondiProject.API.Models.Domains
{
    public class GymMachine
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? ImageName { get; set; }

        public List<GymMaschineInformation>? Informations { get; set; }

        public Gym Gym { get; set; } = null!;

        public int GymId { get; set; }
    }
}
