namespace KondiProject.API.Models.Domains
{
    public class GymMachine
    {
        public int Id { get; set; }

        public string MachineName { get; set; } = null!;

        public List<GymMaschineInformation>? Informations { get; set; }

        public Gym Gym { get; set; } = null!;

        public int GymId { get; set; }
    }
}
