using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class IncrementCounter : PageModel
    {
        public int Counter { get; set; } = 0;

        public void OnGet()
        {
            if (Request.Query.ContainsKey("count"))
            {
                Counter = Convert.ToInt32(Request.Query["count"].First());
            }

            Counter++;
        }
    }
}
