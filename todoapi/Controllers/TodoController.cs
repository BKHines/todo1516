using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using todoapi.Entities;

namespace todoapi.Controllers
{
    [Produces("application/json")]
    [EnableCors("TODOCORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        [HttpGet("GetTodos", Name = "GetTodos")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("GetTodo", Name = "GetTodo")]
        public IActionResult Get(int id) 
        {
            return Ok();
        }

        [HttpPost("AddTodo", Name = "AddTodo")]
        public IActionResult Post([FromBody] TodoItem item)
        {
            return Ok();
        }

        [HttpPut("UpdateTodo", Name = "UpdateTodo")]
        public IActionResult Put([FromBody] TodoItem item, int id)
        {
            return Ok();
        }

        [HttpDelete("DeleteTodo", Name = "DeleteTodo")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
