using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class GetRequest : PageModel
    {
        public string Name { get; set; } = "";

        public void OnGet([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "";
            }
            
            Name = name;
        }
    }
}