using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, AppUserDto>()
            .ForMember(d => d.FullName, u => u.MapFrom(_ => _.FirstName + " " + _.LastName));
    }
}