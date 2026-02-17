using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.DTOs;
using TodoApp.Application.Services;

namespace TodoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(TodoService todoService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodos()
        {
            var todos = await todoService.GetAllTasksAsync();
            return Ok(todos);
        }

        [HttpPost]
        public async Task<ActionResult<TodoDto>> CreateTodo(CreateTodoDto dto)
        {
            var result = await todoService.CreateTaskAsync(dto);
            // 作成されたリソースの場所（URL）を返すのがRESTの基本
            return CreatedAtAction(nameof(GetTodos), new { id = result.Id }, result);
        }

        [HttpPatch("{id}/toggle")]
        public async Task<ActionResult> ToggleTodo(int id)
        {
            await todoService.ToggleTaskAsync(id);
            return NoContent(); // 成功したが返す内容がない場合は204 No Contentを返す
        }
    }
}
