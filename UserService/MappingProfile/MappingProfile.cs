using AutoMapper;
using UserService.DTOs;
using UserService.Interfaces.Models;

namespace UserService.MappingProfile
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserDTO, UserModel>()
                .ForMember(dest => dest.UserId, dto => dto.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, dto => dto.MapFrom(src => src.Name));

            CreateMap<UserModel, UserDTO>()
                .ForMember(dest => dest.UserId, dto => dto.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, dto => dto.MapFrom(src => src.Name));
        }
    }
}
