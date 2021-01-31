using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercises.Pages.Lesson1
{
    public class Assignment5 : PageModel
    {
        public class MoodCounter {
            public int Happy { get; set; }
            public int Disappointed { get; set; }
            public int Angry { get; set; }
        }

        public void OnGet()
        {
        }
    }
}
