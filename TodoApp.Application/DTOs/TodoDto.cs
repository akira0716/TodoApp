// TodoApp.Application/DTOs/TodoDto.cs
namespace TodoApp.Application.DTOs;

public record TodoDto(int Id, string Title, bool IsCompleted);

public record CreateTodoDto(string Title);