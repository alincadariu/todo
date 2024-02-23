using WorkSharp.Storage.Models;

namespace WorkSharp.Storage;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetTodosAsync();
    Task<Todo?> GetTodoAsync(Guid todoId);
    Task CreateTodoAsync(Todo todo);
    Task UpdateTodoAsync(Todo todo);
    Task DeleteTodoAsync(Guid todoId);
}