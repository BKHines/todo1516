using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using todoapi.Entities;

namespace todoapi.Core
{
    public class TodoCore
    {
        public ConcurrentDictionary<int, TodoItem> _todoCollection {get;set;}
        private ILogger _logger;
        public TodoCore(ILogger logger)
        {
            _logger = logger;
            _todoCollection = new ConcurrentDictionary<int, TodoItem>();
            // _logger.LogInformation($"Constructor - {string.Join(",", JsonSerializer.Serialize(_todoCollection))}");
        }

        public TodoItem? GetTodoItem(int id)
        {
            var success = _todoCollection.TryGetValue(id, out TodoItem? tdi);
            // _logger.LogInformation($"GetTodoItem - {string.Join(",", JsonSerializer.Serialize(_todoCollection))}");
            // _logger.LogInformation($"GetTodoItem - {id}: {JsonSerializer.Serialize(tdi)}");
            if (!success)
            {
                return null;
            }

            return tdi;
        }

        public IEnumerable<TodoItem> GetTodoItems()
        {
            return _todoCollection.Select(a => a.Value);
        }

        public int AddTodoItem(TodoItem tdi)
        {
            if (_todoCollection.ContainsKey(-1)) {
                _todoCollection.Remove(-1, out _);
            }
            // _logger.LogInformation($"AddTodoItem1 - {string.Join(",", JsonSerializer.Serialize(_todoCollection))}");
            tdi.id = _todoCollection.Any() && _todoCollection.Last().Key > 0 ? _todoCollection.Last().Key + 1 : 1;
            tdi.status = "synced";
            tdi.updated = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
            // var success = TodoCollection.TryAdd(tdi.id, tdi);
            // if (!success)
            // {
            //     throw new Exception($"Add of {tdi.description} at id {tdi.id} failed");
            // }
            // _logger.LogInformation($"AddTodoItem2 - {string.Join(",", JsonSerializer.Serialize(_todoCollection))}");
            _todoCollection.AddOrUpdate(tdi.id, tdi, (nv, ov) => ov = tdi);
            // _logger.LogInformation($"AddTodoItem3 - {string.Join(",", JsonSerializer.Serialize(_todoCollection))}");
            return tdi.id;
        }

        public bool UpdateTodoItem(TodoItem tdi)
        {
            _todoCollection[tdi.id] = tdi;
            tdi.status = "synced";
            tdi.updated = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff a");
            return true;
        }

        public bool DeleteTodoItem(int id)
        {
            var tdi = new TodoItem();
            return _todoCollection.TryRemove(id, out tdi);
        }
    }


}