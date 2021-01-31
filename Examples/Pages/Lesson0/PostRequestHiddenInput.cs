using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Examples.Pages.Lesson0
{
    public class PostRequestHiddenInput : CarterModule
    {
        public PostRequestHiddenInput()
        {
            Get("/Lesson0/PostRequestHiddenInput", Get);
            Post("/Lesson0/PostRequestHiddenInput", Post);
        }

        private static string DisplayForm(int value)
        {
            return $@"
                {value}
                <form method='post'>
                    <input type='hidden' name='counter' value='{value}'/>
                    <button name='action' value='Decrement'>--</button>                    
                </form>

                <form method='post'>
                    <input type='hidden' name='counter' value='{value}'/>
                    <button name='action' value='Increment'>++</button>
                </form>
            ";
        }

        public static Task Get(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "text/html";

            string content = DisplayForm(0);

            return response.WriteAsync(content);
        }

        public static Task Post(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "text/html";

            int counter = int.Parse(
                            request.Form["counter"].First()
                        );

            string action = WebUtility.HtmlEncode(request.Form["action"].First());

            if (action == "Increment")
            {
                counter++;
            }
            else if (action == "Decrement")
            {
                counter--;
            }
            else
            {
                throw new ArgumentException("Incorrect action value");
            }

            string content = DisplayForm(counter);

            return response.WriteAsync(content);
        }
    }
}
