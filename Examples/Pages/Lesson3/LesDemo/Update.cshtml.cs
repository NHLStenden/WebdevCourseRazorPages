using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson3.LesDemo
{
    public class Update : PageModel
    {
        [BindProperty] public Todo EditTodo { get; set; }
        
        public void OnGet(int todoId)
        {
            EditTodo = new TodoRepository().Get(todoId);
        }

        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                new TodoRepository().Update(EditTodo);
                return RedirectToPage("Index");    
            }

            return Page();
        }
    }
}