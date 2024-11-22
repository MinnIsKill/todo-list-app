using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static readonly List<TodoItem> TodoItems = new List<TodoItem>();

        // GET: api/todo
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAll([FromQuery] int? state)
        {
            var filteredTodos = state.HasValue
                ? TodoItems.Where(t => t.State == state.Value).ToList()
                : TodoItems;

            return Ok(filteredTodos);
        }

        // GET: api/todo/{id}
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(Guid id)
        {
            var todo = TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
                return NotFound(new { isError = true, error = new { code = "404", message = "Todo item not found" } });

            return Ok(todo);
        }

        // POST: api/todo
        [HttpPost]
        public ActionResult<TodoItem> Create([FromBody] TodoItem todo)
        {
            TodoItems.Add(todo);
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }

        // PUT: api/todo/{id}
        [HttpPut("{id}")]
        public ActionResult Update(Guid id, [FromBody] TodoItem todo)
        {
            var existingTodo = TodoItems.FirstOrDefault(t => t.Id == id);
            if (existingTodo == null)
                return NotFound(new { isError = true, error = new { code = "404", message = "Todo item not found" } });

            existingTodo.Title = todo.Title;
            existingTodo.Content = todo.Content;
            existingTodo.State = todo.State;

            return NoContent();
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var todo = TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
                return NotFound(new { isError = true, error = new { code = "404", message = "Todo item not found" } });

            TodoItems.Remove(todo);
            return NoContent();
        }
    }
}