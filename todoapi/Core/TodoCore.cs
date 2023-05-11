using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using todoapi.Entities;

namespace todoapi.Core
{
    public class TodoCore
    {
        public ConcurrentDictionary<int, TodoItem> TodoCollection {get;set;} = new ConcurrentDictionary<int, TodoItem>();
        private ILogger _logger;
        public TodoCore(ILogger logger)
        {
            _logger = logger;
        }
        public TodoItem? GetTodoItem(int id)
        {
            var success = TodoCollection.TryGetValue(id, out TodoItem? tdi);
            _logger.LogInformation($"GetTodoItem - {string.Join(",", JsonSerializer.Serialize(TodoCollection))}");
            _logger.LogInformation($"GetTodoItem - {id}: {JsonSerializer.Serialize(tdi)}");
            if (!success)
            {
                return null;
            }

            return tdi;
        }

        public IEnumerable<TodoItem> GetTodoItems()
        {
            return TodoCollection.Select(a => a.Value);
        }

        public int AddTodoItem(TodoItem tdi)
        {
            tdi.id = TodoCollection.Any() ? TodoCollection.Last().Key + 1 : 1;
            var success = TodoCollection.TryAdd(tdi.id, tdi);
            if (!success)
            {
                throw new Exception($"Add of {tdi.description} at id {tdi.id} failed");
            }
            _logger.LogInformation($"AddTodoItem - {string.Join(",", JsonSerializer.Serialize(TodoCollection))}");
            tdi.status = "synced";
            tdi.updated = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
            return tdi.id;
        }

        public bool UpdateTodoItem(TodoItem tdi)
        {
            TodoCollection[tdi.id] = tdi;
            tdi.status = "synced";
            tdi.updated = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff a");
            return true;
        }

        public bool DeleteTodoItem(int id)
        {
            var tdi = new TodoItem();
            return TodoCollection.TryRemove(id, out tdi);
        }
    }


}