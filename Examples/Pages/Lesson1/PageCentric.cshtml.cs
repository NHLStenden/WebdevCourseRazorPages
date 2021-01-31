using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class PageCentricExample : PageModel
    {
        public class Todo
        {
            public int TodoId { get; set; }
            public string Name { get; set; }
            public bool Completed { get; set; }
        }

        public List<Todo> Todos { get; set; }

        public void OnGet()
        {
            Todos = new List<Todo>();

            for (int i = 1; i <= 10; i++)
            {
                var todo = new Todo();
                todo.TodoId = i;
                todo.Name = "Item " + i;
                todo.Completed = i % 2 == 0;

                Todos.Add(todo);
            }
        }

    }
}
