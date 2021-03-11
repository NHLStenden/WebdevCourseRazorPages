using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson3.LesDemo
{
    public class TodoListDB : PageModel
    {
        public IEnumerable<Todo> Todos
        {
            get { return new TodoRepository().Get(); }
        }

        public void OnGet()
        {
        }

        public void OnPostDelete(int todoId)
        {
            new TodoRepository().Delete(todoId);
        }

        [BindProperty] public Todo NewTodo { get; set; }
        
        public IActionResult OnPostCreate()
        {
            if (ModelState.IsValid)
            {
                var addedTodo = new TodoRepository().AddPlusSelect(NewTodo);
                //do something useful with addedTodo
            }

            return Page();
        }
    }
}