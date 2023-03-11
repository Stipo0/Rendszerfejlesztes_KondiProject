using KondiProject.API.Enums;

namespace KondiProject.API.Models.Domains
{
    public class GymMaschineInformation
    {
        public int Id { get; set; }

        public DataType DataType { get; set; }

        public string Information { get; set; } = null!;

        public GymMachine GymMachine { get; set; } = null!;

        public int GymMachineId { get; set; }
    }
}
