using System.Text.Json;
using WorkSharp.Storage;
using WorkSharp.Storage.Models;

namespace WorkSharp.FileSystem;


public class TodoFileRepository : ITodoRepository
{
    private string _TodoTempPath;

    public TodoFileRepository()
    {
        var tempFolder = "./todos";
        var osTempPath = Path.GetTempPath();

        if (string.IsNullOrEmpty(tempFolder))
        {
            throw new ArgumentException("StoreSettings:FolderPath is not set");
        }

        _TodoTempPath = Path.Combine(osTempPath, tempFolder);

        if (!Directory.Exists(_TodoTempPath))
        {
            Directory.CreateDirectory(_TodoTempPath);
        }

        Console.WriteLine($"StoreFolderPath: {_TodoTempPath}");
    }


    public async Task CreateTodoAsync(Todo todo)
    {
        // TODO: serialize async
        var json = JsonSerializer.Serialize(todo);
        var path = Path.Combine(_TodoTempPath, $"{todo.Id}.json");
        Console.WriteLine(json);
        await File.WriteAllTextAsync(path, json);
    }

    public async Task DeleteTodoAsync(Guid todoId)
    {
        await Task.Run(() => File.Delete(Path.Combine(_TodoTempPath, $"{todoId}.json")));
    }

    public Task<Todo> GetTodoAsync(Guid todoId)
    {

        var file = Directory.GetFiles(_TodoTempPath, $"{todoId}.json").FirstOrDefault();
        if (file == null)
        {
            throw new Exception("Todo not found");
        }

        var content = File.ReadAllText(file);
        var todo = JsonSerializer.Deserialize<Todo>(content);

        if (todo == null)
        {
            throw new Exception("Corrupted file");
        }

        return Task.FromResult(todo);
    }

    public async Task<IEnumerable<Todo>> GetTodosAsync()
    {
        var files = Directory.GetFiles(_TodoTempPath)
            .Select(async file =>
            {
                var info = new FileInfo(file);
                var content = await File.ReadAllTextAsync(file);
                var todo = JsonSerializer.Deserialize<Todo>(content);

                Console.WriteLine(info.Name);
                if (info.Name != $"{todo.Id}.json")
                {
                    throw new Exception("Corrupted file");
                }

                // TODO: validate model

                return todo;
            })
            .Select(task => task.Result)
            .Where(todo => todo != null);

        return files;
    }

    public async Task UpdateTodoAsync(Todo todo)
    {
        Console.WriteLine(todo.Checked);
        await CreateTodoAsync(todo);
    }
}
