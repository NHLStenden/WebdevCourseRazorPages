using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class HiddenFormFields : PageModel
    {
        public void OnGet()
        {

        }

        [BindProperty]
        public string Action { get; set; }

        public int ProductId { get; set; }

        public void OnPost([FromForm]int productId)
        {
            ProductId = productId;
        }
    }
}
