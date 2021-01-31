using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class QueryStringsMethodRequest : PageModel
    {
        public string Firstname { get; set; }

        public IEnumerable<string> MiddleNames { get; set; } = new List<string>();

        public string Lastname { get; set; }

        //[FromQuery] string firstname, [FromQuery]IEnumerable<string> middleNames, [FromQuery]string lastname
        public void OnGet()
        {
            Firstname = Request.Query["firstname"];
            MiddleNames = Request.Query["middleNames"].Where(x => !string.IsNullOrWhiteSpace(x));
            Lastname = Request.Query["lastname"];
        }


    }
}
