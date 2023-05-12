using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using todoapi.Core;
using todoapi.Entities;
using todoapi.Managers;

namespace todoapi.Controllers
{
    [Produces("application/json")]
    [EnableCors("TODOCORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private TodoManager _todoMgr;
        private ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger)
        {
            _todoMgr = new TodoManager(logger);
            _logger = logger;
        }

        [HttpGet("GetTodos", Name = "GetTodos")]
        public IActionResult Get()
        {
            return Ok(_todoMgr.GetTodoItems());
        }

        [HttpGet("GetTodo", Name = "GetTodo")]
        public IActionResult Get(int id) 
        {
            var _tdi = _todoMgr.GetTodoItem(id);
            if (_tdi == null)
            {
                _tdi = new TodoItem();
            }
            return Ok(_tdi);
        }

        [HttpPost("AddTodo", Name = "AddTodo")]
        public IActionResult Post([FromBody] TodoItem item)
        {
            // _logger.LogInformation(JsonSerializer.Serialize(item));
            // _logger.LogInformation($"AddTodoCtrl - {string.Join(",", JsonSerializer.Serialize(TodoContext.ApplicationTodoCore._todoCollection))}");
            return Ok(_todoMgr.AddTodoItem(item));
        }

        [HttpPut("UpdateTodo", Name = "UpdateTodo")]
        public IActionResult Put([FromBody] TodoItem item, int id)
        {
            return Ok(_todoMgr.UpdateTodoItem(item));
        }

        [HttpPost("UpdateTodos", Name = "UpdateTodos")]
        public IActionResult UpdateTodos([FromBody] IEnumerable<TodoItem> items) 
        {
            var rv = true;
            foreach (var item in items)
            {
                rv = rv && _todoMgr.UpdateTodoItem(item);
            }
            return Ok(rv);
        }
        
        [HttpDelete("DeleteTodo", Name = "DeleteTodo")]
        public IActionResult Delete(int id)
        {
            return Ok(_todoMgr.DeleteTodoItem(id));
        }
    }
}
