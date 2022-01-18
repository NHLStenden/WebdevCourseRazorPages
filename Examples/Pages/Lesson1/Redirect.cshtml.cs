using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class Redirect : PageModel
    {
        public int Count { get; set; } = 0;
        
        
        public IActionResult OnGet(int? countParameter)
        {
            if (countParameter != null)
            {
                Count = countParameter.Value;
            }

            Count++;

            if (Count < 10)
            {
                return RedirectToPage("Redirect", new
                {
                    countParameter = Count
                });
            } 
            else
            {
                return Page();
            }
        }
    }
}
