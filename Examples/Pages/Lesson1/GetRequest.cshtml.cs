using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class GetRequest : PageModel
    {
        public string Firstname { get; set; } = "";

        public void OnGet([FromQuery] string firstname)
        {
            if (string.IsNullOrWhiteSpace(firstname))
            {
                firstname = "";
            }
            
            Firstname = firstname;
        }
    }
}