using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson2.LesDemo
{
    public class TodoList : PageModel
    {
        public IEnumerable<Todo> Todos { get; set; } = new TodoRepository().Get();

        public void OnGet()
        {

        }

        public void OnPostDelete(int todoId)
        {
            new TodoRepository().Delete(todoId);
        }

        // public void OnPostCreate([FromForm]Todo todo)
        // {
        //     new TodoRepository().Add(todo);
        // }

        [BindProperty]
        public Todo NewTodo { get; set; }

        public IActionResult OnPostCreate()
        {
            if (ModelState.IsValid)
            {
                new TodoRepository().Add(NewTodo);
            }

            return Page();
        }
    }
}
