using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class GetRequestQueryString : PageModel
    {
        public string NameFromRequestQuery { get; set; }
        public string NameFromParameter { get; set; }
        
        [BindProperty(SupportsGet = true, Name="naam")]
        public string NameFromPropertyBinding { get; set; }
        
        public void OnGet([FromQuery] string naam)
        {
            NameFromParameter = naam;
            NameFromRequestQuery = Request.Query["name"];
        }
    }
}
