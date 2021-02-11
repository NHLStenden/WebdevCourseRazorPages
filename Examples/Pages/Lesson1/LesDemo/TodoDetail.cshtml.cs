using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1.LesDemo
{
    public class TodoDetail : PageModel
    {
        public Todo Todo { get; set; }

        public void OnGet([FromRoute]int todoId)
        {
            Todo = new TodoRepository().Get(todoId);
        }
    }
}
