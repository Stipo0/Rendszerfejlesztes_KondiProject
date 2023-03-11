using AutoMapper;
using KondiProject.API.Models.Domains;
using KondiProject.API.Models.Dtos.Requests;
using KondiProject.API.Models.Dtos.Responses;

namespace KondiProject.API.Models.AutoMapperProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserRegisterRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
