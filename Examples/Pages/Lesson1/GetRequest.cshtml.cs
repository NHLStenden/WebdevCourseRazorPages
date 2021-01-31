using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class GetRequest : PageModel
    {
        public string Name { get; set; }

        public void OnGet()
        {
            if (Request.Query.ContainsKey("naam") && !string.IsNullOrWhiteSpace(Request.Query["naam"]))
            {
                Name = Request.Query["naam"].First().ToUpper();
            }
        }
    }
}
