using AutoMapper;
using WorkSharp.Storage.Models;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<Todo, ReadTodoDto>();
        CreateMap<CreateTodoDto, Todo>();
    }
}