using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercises.Pages.Lesson1
{


    public class Exercise2 : PageModel
    {
        public enum Direction
        {
            Left, Right, Forward, Backward
        }

        public void OnGet()
        {
        }
    }
}
