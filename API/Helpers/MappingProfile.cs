using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Excel;

namespace API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, AppUserDto>()
            .ForMember(
                d => d.FullName,
                u => u.MapFrom(_ => _.FirstName + " " + _.LastName));
        CreateMap<SubTaskDto, SubTask>();
        CreateMap<Board, ExcelBoard>();
        CreateMap<Todo, ExcelTodo>();
        CreateMap<SubTask, ExcelSubTask>()
            .ForMember(
                d => d.IsDone,
                u => u.MapFrom(_ => _.IsDone ? "Yes" : "No"));
    }
}