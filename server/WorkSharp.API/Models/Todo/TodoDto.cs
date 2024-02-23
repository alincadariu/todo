public class ReadTodoDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime Timestamp { get; set; }
    public bool Checked { get; set; }
}