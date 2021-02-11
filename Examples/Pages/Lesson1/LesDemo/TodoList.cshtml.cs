using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1.LesDemo
{
    public class TodoList : PageModel
    {
        public List<Todo> Todos { get; set; } = new TodoRepository().Get();

        public void OnGet([FromRoute] string filter)
        {
        }

        [BindProperty] public Todo NewTodo { get; set; } = new Todo();

        public void OnPost([FromForm] int todoId, [FromForm]string action = "")
        {
            switch (action.ToLower())
            {
                case "add":
                    //let op de Done werkt niet! We gaan dit oplossen!
                    new TodoRepository().Add(NewTodo);
                break;
                case "delete":
                    new TodoRepository().Delete(todoId);
                break;
            }
        }
    }
}
