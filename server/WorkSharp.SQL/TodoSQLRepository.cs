using Microsoft.EntityFrameworkCore;
using WorkSharp.SQL;
using WorkSharp.SQL.Models;
using WorkSharp.Storage;
using WorkSharp.Storage.Models;

namespace WorkShop.SQL;

public class TodoSQLRepository : ITodoRepository
{
    private readonly TodoContext _db = new TodoContext();

    public async Task<IEnumerable<WorkSharp.Storage.Models.Todo>> GetTodosAsync()
    {
        return await _db.Todos
            .Select(todo => new WorkSharp.Storage.Models.Todo
            {
                Id = todo.Id,
                Name = todo.Name,
                Checked = todo.Checked,
                Timestamp = todo.CreatedAt,
                UserId = todo.Owner.Id,
            })
            .ToListAsync();
    }

    public async Task<WorkSharp.Storage.Models.Todo?> GetTodoAsync(Guid id)
    {
        return await _db.Todos
            .Select(todo => new WorkSharp.Storage.Models.Todo
            {
                Id = todo.Id,
                Name = todo.Name,
                Checked = todo.Checked,
                Timestamp = todo.CreatedAt,
                UserId = todo.Owner.Id,
            })
            .FirstOrDefaultAsync(todo => todo.Id == id);
    }

    public async Task CreateTodoAsync(WorkSharp.Storage.Models.Todo todo)
    {
        var owner = await _db.Users.FindAsync(todo.UserId);

        // if (owner == null)
        // {
        //     _db.Users.Add(new User { Id = todo.UserId, Email = "", FullName = "" });
        // }

        var entry = new WorkSharp.SQL.Models.Todo
        {
            Id = Guid.NewGuid(),
            Name = todo.Name,
            Checked = todo.Checked,
            CreatedAt = DateTime.Now,
            Owner = owner,
        };

        _db.Todos.Add(entry);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateTodoAsync(WorkSharp.Storage.Models.Todo todo)
    {
        var entry = await _db.Todos.FindAsync(todo.Id);

        if (entry == null)
        {
            throw new Exception("Todo not found");
        }

        entry.Name = todo.Name;
        entry.Checked = todo.Checked;

        _db.Entry(entry).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }


    public async Task DeleteTodoAsync(Guid id)
    {
        var entry = await _db.Todos.FindAsync(id);

        if (entry == null)
        {
            throw new Exception("Todo not found");
        }
        _db.Todos.Remove(entry);
        await _db.SaveChangesAsync();
    }

}
