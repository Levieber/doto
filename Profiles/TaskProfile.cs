using AutoMapper;
using doto.Data.Dtos;
using doto.Models;

namespace doto.Profiles;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<CreateTaskDto, TaskModel>();
        CreateMap<UpdateTaskDto, TaskModel>();
        CreateMap<TaskModel, UpdateTaskDto>();
    }
}
