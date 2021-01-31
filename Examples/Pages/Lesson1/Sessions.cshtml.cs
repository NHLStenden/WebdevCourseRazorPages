using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{

    //https://www.learnrazorpages.com/razor-pages/session-state
    public class Sessions : PageModel
    {
        public int Count { get; set; }

        public void OnGet()
        {
            string strCount = HttpContext.Session.GetString("count");
            if (strCount != null)
            {
                Count = Convert.ToInt32(strCount);
                Count++;
            }
            else
            {
                Count = 0;
            }

            HttpContext.Session.SetString("count", Count.ToString());
        }
    }
}
