using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _svc;

    public TodoController(ITodoService svc)
    {
        _svc = svc;
    }

    [HttpGet(Name = "GetTodo")]
    public async Task<IEnumerable<ReadTodoDto>> Get()
    {
        return await _svc.GetTodosAsync();
    }

    [HttpPost(Name = "CreateTodo")]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] CreateTodoDto dto)
    {
        var todo = await _svc.CreateTodoAsync(dto, User.Id());
        return CreatedAtRoute("GetTodo", new { todoId = todo.Id }, todo);
    }

    [HttpDelete("{todoId}", Name = "DeleteTodo")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid todoId)
    {

        await _svc.DeleteTodoAsync(todoId);
        return Ok();
    }

    [HttpPut("{todoId}", Name = "UpdateTodo")]
    [Authorize]
    public async Task<IActionResult> Put(Guid todoId, [FromBody] UpdateTodoDto dto)
    {
        if (!await _svc.IsOwner(todoId, User.Id()))
        {
            return Unauthorized("You are not the owner of this todo!");
        }

        await _svc.UpdateTodoAsync(todoId, dto);
        return Ok();
    }
}