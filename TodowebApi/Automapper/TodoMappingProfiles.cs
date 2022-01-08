using AutoMapper;
using TodowebApi.Dtos;
using TodowebApi.Models;

namespace TodowebApi.Automapper
{
    public class TodoMappingProfiles : Profile
    {
        public TodoMappingProfiles()
        {
            CreateMap<TodoCreateDto, Todo>();
            CreateMap<Todo, TodoGetDto>();
        }
    }
}
