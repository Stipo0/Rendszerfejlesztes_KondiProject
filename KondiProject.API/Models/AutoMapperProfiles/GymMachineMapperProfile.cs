using AutoMapper;
using KondiProject.API.Models.Domains;
using KondiProject.API.Models.Dtos.Requests;
using KondiProject.API.Models.Dtos.Responses;

namespace KondiProject.API.Models.AutoMapperProfiles
{
    public class GymMachineMapperProfile : Profile
    {
        public GymMachineMapperProfile()
        {
            CreateMap<CreateGymMachineRequest, GymMachine>();
            CreateMap<GymMachine, GymMachineResponse>();
        }
    }
}
