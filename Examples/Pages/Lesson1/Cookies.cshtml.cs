using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class Cookies : PageModel
    {
        public int Counter { get; set; } = 0;

        public void OnGet(string action = "")
        {
            action = action.ToLower();

            string counterStr = Request.Cookies["counter"];
            if (counterStr == null) //cookie is not set (first time request or after reset)
            {
                Response.Cookies.Append("counter", Counter.ToString(), new CookieOptions()
                {
                    Expires = DateTimeOffset.Now.AddDays(30)
                });
                //Response.Cookies.Append("counter", Counter.ToString());
            }
            else
            {
                Counter = int.Parse(counterStr);  //Convert.ToInt32(counterStr)

                if (action == "increment")
                {
                    Counter++;
                    Response.Cookies.Append("counter", Counter.ToString());
                } else if (action == "decrement")
                {
                    Counter--;
                    Response.Cookies.Append("counter", Counter.ToString());
                } else if (action == "reset")
                {
                    //counter = 0;
                    Response.Cookies.Delete("counter");
                }
            }
        }
    }
}
