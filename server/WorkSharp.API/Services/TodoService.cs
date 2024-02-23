using AutoMapper;
using WorkSharp.Storage;
using WorkSharp.Storage.Models;
public class TodoService : ITodoService
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _todo;

    public TodoService(IMapper mapper, ITodoRepository todo)
    {
        _mapper = mapper;
        _todo = todo;
    }

    public async Task<ReadTodoDto> CreateTodoAsync(CreateTodoDto dto, string userId)
    {
        var todo = _mapper.Map<Todo>(dto);
        todo.UserId = userId;

        if (todo == null)
        {
            throw new ArgumentNullException(nameof(todo));
        }

        await _todo.CreateTodoAsync(todo);

        return _mapper.Map<ReadTodoDto>(todo);
    }

    public async Task<IEnumerable<ReadTodoDto>> GetTodosAsync()
    {
        return _mapper.Map<IEnumerable<ReadTodoDto>>(await _todo.GetTodosAsync());
    }

    public async Task DeleteTodoAsync(Guid todoId)
    {
        await _todo.DeleteTodoAsync(todoId);
    }

    public async Task UpdateTodoAsync(Guid todoId, UpdateTodoDto dto)
    {
        var todo = await _todo.GetTodoAsync(todoId);

        todo.Checked = dto.Checked;

        await _todo.UpdateTodoAsync(todo);
    }

    public async Task<bool> IsOwner(Guid todoId, string userId)
    {
        var todo = await _todo.GetTodoAsync(todoId);

        if (todo == null)
        {
            throw new ArgumentNullException(nameof(todo));
        }

        return todo.UserId == userId;
    }
}