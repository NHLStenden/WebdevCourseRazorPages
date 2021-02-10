using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson3.LesDemo
{
    public class TodoListDB : PageModel
    {
        public List<Todo> Todos
        {
            get
            {
                return new TodoRepository().Get();
            }
        }

        public void OnGet()
        {
        }

        public void OnPostDelete(int todoId)
        {
            new TodoRepository().Delete(todoId);
        }

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
