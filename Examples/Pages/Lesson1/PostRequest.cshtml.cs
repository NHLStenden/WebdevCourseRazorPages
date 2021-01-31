using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class PostRequest : PageModel
    {
        public string Name { get; set; }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (Request.Query.ContainsKey("naam") && !string.IsNullOrWhiteSpace(Request.Query["naam"]))
            {
                Name = Request.Form["naam"].First().ToUpper();
            }
        }
    }
}
