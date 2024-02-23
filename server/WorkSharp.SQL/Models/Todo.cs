namespace WorkSharp.SQL.Models;

public class Todo
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool Checked { get; set; } = false;
    public required DateTime CreatedAt { get; set; }
    public required User Owner { get; set; }
}
