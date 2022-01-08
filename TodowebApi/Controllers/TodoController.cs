using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodowebApi.Data;
using TodowebApi.Dtos;
using TodowebApi.Models;

namespace TodowebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly DataContext _ctx;
        private readonly IMapper _mapper;

        public TodoController(DataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetTodo()
        {
            var todo = await _ctx.Todos.ToListAsync();
            var productGet = _mapper.Map<List<TodoGetDto>>(todo);
            return Ok(productGet);
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var todo = await _ctx.Todos.FirstOrDefaultAsync(h => h.TodoId == id);
            if (todo == null)
            {
                return NotFound();
            }
            var todoGet = _mapper.Map<TodoGetDto>(todo);
            return Ok(todoGet);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoCreateDto todo)
        {
            var domainTodo = _mapper.Map<Todo>(todo);

            _ctx.Todos.Add(domainTodo);
           
            await _ctx.SaveChangesAsync();

            var todoGet = _mapper.Map<TodoGetDto>(domainTodo);

            return CreatedAtAction(nameof(GetTodoById), new { id = domainTodo.TodoId }, todoGet);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTodo([FromBody] TodoCreateDto update, int id)
        {
            var toUpdate = _mapper.Map<Todo>(update);
            toUpdate.TodoId = id;

            _ctx.Todos.Update(toUpdate);

            await _ctx.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _ctx.Todos.FirstOrDefaultAsync(h => h.TodoId == id);
            if (todo == null)
            {
                return NotFound();
            }
            _ctx.Todos.Remove(todo);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
