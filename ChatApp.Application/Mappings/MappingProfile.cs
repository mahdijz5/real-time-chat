
using AutoMapper;
using ChatApp.Application.Dtos;
using ChatApp.Domain.Entities;

namespace ChatApp.Domain.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserResDto>();
    }
}