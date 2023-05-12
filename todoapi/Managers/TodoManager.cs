using todoapi.Entities;
using todoapi.Core;
using System.Text.Json;
using System.Diagnostics;

namespace todoapi.Managers
{
    public class TodoManager
    {
        private ILogger _logger;

        public TodoManager(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<TodoItem> GetTodoItems()
        {
            return TodoContext.ApplicationTodoCore.GetTodoItems();
        }

        public TodoItem? GetTodoItem(int id)
        {
            return TodoContext.ApplicationTodoCore.GetTodoItem(id);
        }

        public int AddTodoItem(TodoItem tdi)
        {
            // _logger.LogInformation(JsonSerializer.Serialize(tdi));
            // _logger.LogInformation($"AddTodoItemMgr - {string.Join(",", JsonSerializer.Serialize(TodoContext.ApplicationTodoCore._todoCollection))}");

            return TodoContext.ApplicationTodoCore.AddTodoItem(tdi);
        }

        public bool UpdateTodoItem(TodoItem tdi)
        {
            return TodoContext.ApplicationTodoCore.UpdateTodoItem(tdi);
        }

        public bool DeleteTodoItem(int id)
        {
            return TodoContext.ApplicationTodoCore.DeleteTodoItem(id);
        }
    }
}