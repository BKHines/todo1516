using todoapi.Entities;
using todoapi.Core;

namespace todoapi.Managers
{
    public class TodoManager
    {
        private TodoCore _todoCore;

        public TodoManager()
        {
            _todoCore = new TodoCore();
        }

        public IEnumerable<TodoItem> GetTodoItems()
        {
            return _todoCore.GetTodoItems();
        }

        public TodoItem? GetTodoItem(int id)
        {
            return _todoCore.GetTodoItem(id);
        }

        public int AddTodoItem(TodoItem tdi)
        {
            return _todoCore.AddTodoItem(tdi);
        }

        public bool UpdateTodoItem(TodoItem tdi)
        {
            return _todoCore.UpdateTodoItem(tdi);
        }

        public bool DeleteTodoItem(int id)
        {
            return _todoCore.DeleteTodoItem(id);
        }
    }
}