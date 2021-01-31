using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class NonWorkingCounter : PageModel
    {
        public int Counter { get; set; }

        public void OnGet()
        {
            Counter++;
            //this counter will never be greater than 1,
            //that's why we need state management to maintain state over multiple HTTP Request!
        }
    }
}
