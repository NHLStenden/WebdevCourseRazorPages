using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class PageCentricSimple : PageModel
    {
        public List<string> Names { get; set; } =
            new List<string>() {
                "Joris", "Jos", "Martin" };

        public IActionResult OnGet([FromQuery]string action = "")
        {
            switch (action.ToLower())
            {
                case "redirect":
                    return Redirect("www.google.com");
                case "badrequest":
                    return BadRequest();
                default:
                    return Page();
            }
        }
    }
}
