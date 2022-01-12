using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class QueryStringsBindProperty : PageModel
    {
        [BindProperty(SupportsGet = true)] public string Firstname { get; set; }

        [BindProperty(SupportsGet = true)] public IEnumerable<string> MiddleNames { get; set; } = new List<string>();

        [BindProperty(SupportsGet = true)] public string Lastname { get; set; }

        public void OnGet()
        {

        }
    }
}
