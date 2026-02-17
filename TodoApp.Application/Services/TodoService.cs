using FluentValidation;
using TodoApp.Application.DTOs;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.Services
{
    public class TodoService(ITodoRepository repository,
                             IValidator<CreateTodoDto> validator)
    {
        public async Task<IEnumerable<TodoDto>> GetAllTasksAsync()
        {
            var items = await repository.GetAllAsync();
            return items.Select(x => new TodoDto(x.Id, x.Title, x.IsCompleted));
        }

        public async Task<TodoDto> CreateTaskAsync(CreateTodoDto dto)
        {
            // バリデーション実行
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                // エラーがあれば例外を投げる
                throw new ValidationException(validationResult.Errors);
            }

            var newItem = new TodoItem { Title = dto.Title };
            await repository.AddAsync(newItem);

            return new TodoDto(newItem.Id, newItem.Title, newItem.IsCompleted);
        }

        public async Task ToggleTaskAsync(int id)
        {
            var item = await repository.GetByIdAsync(id);
            if (item != null)
            {
                item.IsCompleted = !item.IsCompleted;
                await repository.UpdateAsync(item);
            }
        }
    }
}
