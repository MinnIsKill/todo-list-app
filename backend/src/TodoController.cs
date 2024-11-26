using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoItemRepository _repository;
        private readonly ILogger<TodoController> _logger;

        public TodoController(TodoItemRepository repository, ILogger<TodoController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll([FromQuery] int? state)
        {
            var todos = await _repository.GetAllTodoItemsAsync(state);
            return Ok(todos);
        }

        // GET: api/todo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetById(Guid id)
        {
            var todo = await _repository.GetTodoItemByIdAsync(id);
            if (todo == null)
            {
                return NotFound(new { isError = "true", error = new { code = "404", message = "Todo item not found" } });
            }
            return Ok(todo);
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> Create([FromBody] TodoItem todo)
        {
            if (todo == null)
            {
                _logger.LogWarning("Received a null TodoItem.");
                return BadRequest(new { isError = "true", error = new { code = "400", message = "Todo item cannot be null" } });
            }

            _logger.LogInformation("Received a request to add TodoItem with Title: {Title}", todo.Title);

            try
            {
                await _repository.AddTodoItemAsync(todo);
                _logger.LogInformation("TodoItem with ID: {Id} added successfully.", todo.Id);
                return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding TodoItem.");
                return StatusCode(500, new { isError = "true", error = new { code = "500", message = "Internal server error" } });
            }
        }

        // PUT: api/todo/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] TodoItem todo)
        {
            _logger.LogInformation("Received a request to update TodoItem with Title: {Title}", todo.Title);

            var existingTodo = await _repository.GetTodoItemByIdAsync(id);
            if (existingTodo == null)
            {
                _logger.LogError("Error occurred while adding TodoItem");
                return NotFound(new { isError = "true", error = new { code = "404", message = "Todo item not found" } });
            }

            await _repository.UpdateTodoItemAsync(todo);
            return Ok(todo);
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Received a request to delete TodoItem with ID: {id}", id);

            var todo = await _repository.GetTodoItemByIdAsync(id);
            if (todo == null)
            {
                _logger.LogInformation("Todo item not found.");
                return NotFound(new { isError = "true", error = new { code = "404", message = "Todo item not found" } });
            }

            await _repository.DeleteTodoItemAsync(id);
            _logger.LogInformation("Todo item deleted successfully.");
            return NoContent();
        }
    }
}