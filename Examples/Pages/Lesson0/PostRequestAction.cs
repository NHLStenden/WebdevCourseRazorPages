using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Examples.Pages.Lesson0
{
    public class PostRequestAction : CarterModule
    {
        public PostRequestAction()
        {
            Get("/Lesson0/PostRequestAction", Get);
            Post("/Lesson0/PostRequestAction/{action}", Post);
        }

        private static string DisplayForm(int value)
        {
            return $@"
                {value}
                <form method='post' action='/lesson0/PostRequestAction/Decrement'>
                    <input type='hidden' name='counter' value='{value}'/>
                    <button>--</button>                    
                </form>

                <form method='post' action='/lesson0/PostRequestAction/Increment'>
                    <input type='hidden' name='counter' value='{value}'/>
                    <button>++</button>    
                </form>
            ";
        }

        private static Task Get(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "text/html";

            string content = DisplayForm(0);

            return response.WriteAsync(content);
        }

        private static Task Post(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "text/html";

            int counter = int.Parse(
                            request.Form["counter"].First()
                        );

            string action = WebUtility.HtmlEncode(request.RouteValues["action"].ToString());

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
