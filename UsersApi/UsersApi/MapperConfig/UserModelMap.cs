using AutoMapper;
using System;
using UsersApi.Dtos;
using UsersApi.Models.Response;

namespace UsersApi.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserResponseModel, UserDto>();
        }
    }
}
