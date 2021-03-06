using System.Collections.Generic;
using System.Linq;

namespace Examples.Pages.Lesson1.LesDemo
{
    public class TodoRepository
    {
        public static List<Todo> Todos { get; set; } = new List<Todo>()
        {
            new Todo() { TodoId = 1, Description = "Item 1", Done = false },
            new Todo() { TodoId = 2, Description = "Item 2", Done = false }
        };

        public void Delete(int todoId)
        {
            var todoToRemove = Todos.FirstOrDefault(x => x.TodoId == todoId);
            if (todoToRemove != null)
            {
                Todos.Remove(todoToRemove);
            }
        }

        public List<Todo> Get()
        {
            return Todos;
        }

        public List<Todo> Get(string filter)
        {
            return Todos.Where(x => x.Description.Contains(filter)).ToList();
        }

        public Todo Get(int todoId)
        {
            return Todos.FirstOrDefault(x => x.TodoId == todoId);
        }

        public void Add(Todo todo)
        {
            todo.TodoId = Todos.Max(x => x.TodoId) + 1;
            Todos.Add(todo);
        }
    }
}
