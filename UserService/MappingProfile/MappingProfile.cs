using AutoMapper;
using UserService.DTOs;
using UserService.Interfaces.Models;

namespace UserService.MappingProfile
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserDataDTO, UserDataModel>()
                .ForMember(dest => dest.UserId, dto => dto.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, dto => dto.MapFrom(src => src.Name));

            CreateMap<UserDataModel, UserDataDTO>()
                .ForMember(dest => dest.UserId, dto => dto.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, dto => dto.MapFrom(src => src.Name));

            CreateMap<UserDTO, UserModel>()
                .ForMember(dest => dest.Id, dto => dto.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, dto => dto.MapFrom(src => src.UserName));

            CreateMap<UserModel, UserDTO>()
                .ForMember(dest => dest.Id, dto => dto.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, dto => dto.MapFrom(src => src.UserName));
        }
    }
}
