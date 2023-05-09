using System.Collections.Concurrent;
using todoapi.Entities;

namespace todoapi.Core
{
    public class TodoCore
    {
        public ConcurrentDictionary<int, TodoItem> TodoCollection {get;set;} = new ConcurrentDictionary<int, TodoItem>();

        public TodoItem? GetTodoItem(int id)
        {
            if (TodoCollection[id] != null)
            {
                return null;
            }

            return TodoCollection[id];
        }

        public IEnumerable<TodoItem> GetTodoItems()
        {
            return TodoCollection.Select(a => a.Value);
        }

        public int AddTodoItem(TodoItem tdi)
        {
            tdi.id = TodoCollection.Any() ? TodoCollection.Last().Key + 1 : 1;
            if (!TodoCollection.TryAdd(tdi.id, tdi))
            {
                throw new Exception($"Add of {tdi.description} at id {tdi.id} failed");
            }
            return tdi.id;
        }

        public bool UpdateTodoItem(TodoItem tdi)
        {
            TodoCollection[tdi.id] = tdi;
            return true;
        }

        public bool DeleteTodoItem(int id)
        {
            var tdi = new TodoItem();
            return TodoCollection.TryRemove(id, out tdi);
        }
    }


}