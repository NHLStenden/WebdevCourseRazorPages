using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class SetTempData : PageModel
    {
        public void OnGet() {
        }

        public RedirectToPageResult OnPost([FromForm]int lievelingsgetal)
        {
            TempData["Lievelingsgetal"] = lievelingsgetal.ToString();
            return RedirectToPage("ShowTempData");
        }
    }
}
