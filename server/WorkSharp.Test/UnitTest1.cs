namespace WorkSharp.Test;

using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Controllers;
using Xunit;

public class TodoControllerTests
{

    private readonly Mock<ITodoService> _mockService;
    private readonly TodoController _controller;

    public TodoControllerTests()
    {
        _mockService = new Mock<ITodoService>();
        _controller = new TodoController(_mockService.Object);
    }

    [Fact]
    public async Task Get_ReturnsTodos()
    {
        // Arrange
        var todos = new List<ReadTodoDto>
        {
            new ReadTodoDto { Id = Guid.NewGuid(), Name = "Todo 1", Timestamp = DateTime.Now, Checked = false},
            new ReadTodoDto { Id = Guid.NewGuid(), Name = "Todo 2", Timestamp = DateTime.Now, Checked = false}
        };
        _mockService.Setup(s => s.GetTodosAsync()).ReturnsAsync(todos);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<List<ReadTodoDto>>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<ReadTodoDto>>(okResult);
        Assert.Equal(2, model.Count());
    }

}
