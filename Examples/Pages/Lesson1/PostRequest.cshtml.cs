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

        public void OnGet()
        {}

        public void OnPost([FromForm] string naam)
        {
            NameRequest = Request.Form["naam"];
            NameParameter = naam;
        }
    }
}
