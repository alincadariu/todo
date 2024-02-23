public interface ITodoService
{
    Task<IEnumerable<ReadTodoDto>> GetTodosAsync();
    Task<ReadTodoDto> CreateTodoAsync(CreateTodoDto dto, string userId);
    Task DeleteTodoAsync(Guid todoId);
    Task UpdateTodoAsync(Guid todoId, UpdateTodoDto dto);
    Task<bool> IsOwner(Guid todoId, string userId);
}