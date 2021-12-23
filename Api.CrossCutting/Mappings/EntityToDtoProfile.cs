using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDTO, UserEntity>().ReverseMap();
            CreateMap<UserCreateResultDTO, UserEntity>().ReverseMap();
            CreateMap<UserUpdateResultDTO, UserEntity>().ReverseMap();
        }
    }
}