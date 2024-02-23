namespace WorkSharp.Storage.Models;

public class Todo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public bool Checked { get; set; } = false;
    public required DateTime Timestamp { get; set; } = DateTime.Now;
    public required string UserId { get; set; }
}