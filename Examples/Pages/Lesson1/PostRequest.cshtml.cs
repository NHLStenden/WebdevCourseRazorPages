using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class PostRequest : PageModel
    {
        public string NameRequest { get; set; }
        public string NameParameter { get; set; }

        [BindProperty(Name = "naam")]
        public string NameBindProperty { get; set; }
        //public string Naam { get; set; }

        public IActionResult OnGet()
        {
            var page = Page();
            return page;
            
        }

        public IActionResult OnPost([FromForm] string naam)
        {
            NameRequest = Request.Form["naam"]; //liever niet gebruiken
            NameParameter = naam;
            return Page();
        }
    }
}
