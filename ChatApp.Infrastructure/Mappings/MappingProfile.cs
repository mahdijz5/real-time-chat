
using AutoMapper;
using ChatApp.Application.Dtos;


using ChatApp.Infrastructure.Model;

namespace ChatApp.Domain.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<UserModel, UserResDto>();
    }
}