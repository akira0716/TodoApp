using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Infrastructure.Data
{
    public class TodoRepository(AppDbContext context) : ITodoRepository
    {
        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await context.TodoItems.FindAsync(id);
        }

        public async Task AddAsync(TodoItem todo)
        {
            await context.TodoItems.AddAsync(todo);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem todo)
        {
            context.TodoItems.Update(todo);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var todo = await context.TodoItems.FindAsync(id);
            if (todo != null)
            {
                context.TodoItems.Remove(todo);
                await context.SaveChangesAsync();
            }
        }
    }
}
