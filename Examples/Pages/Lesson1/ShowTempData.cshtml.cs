using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class ShowTempData : PageModel
    {
        [TempData] public string Lievelingsgetal { get; set; } = null;

        public IActionResult OnGet()
        {
            if (string.IsNullOrWhiteSpace(Lievelingsgetal))
            {
                return RedirectToPage("SetTempData");
            }

            return Page();
        }
    }
}
