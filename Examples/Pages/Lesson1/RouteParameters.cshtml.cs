using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    //Let op de @page attribute en optionele argumenten!
    // https://localhost:5001/Lesson1/RouteParameters/increment/1
    // https://www.learnrazorpages.com/razor-pages/routing
    // https://www.mikesdotnetting.com/article/339/optional-parameters-in-razor-pages-routing#:~:text=Razor%20Pages%20routing%20is%20based,to%20permit%20multiple%20optional%20parameters.
    public class RouteParameters : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Count { get; set; } = 0;

        public void OnGet([FromRoute]string action)
        {
            if (action == "")
            {
                Count = 0;
            }
            else if (action == "increment")
            {
                Count++;
            }
            else if (action == "decrement")
            {
                Count--;
            }
        }
    }
}
