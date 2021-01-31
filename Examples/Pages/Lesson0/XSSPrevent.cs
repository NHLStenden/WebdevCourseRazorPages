using System.Net;
using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Examples.Pages.Lesson0
{
    //Submit the following value in the textbox (name):
    //<script type="text/javascript">alert("text");</script>

    //Submit the following value in the textbox (name):
    //<h1><b>Test</b></h1>

    //to prevent XSS use WebUtility.HtmlEncode(request.Query["name"])
    //not only with request.Query[...], with everything that is send by the client
    //for example:
    //request.Form
    //request.Cookies
    //request.Query, request.QueryString
    //be careful!
    public class XSSPrevent : CarterModule
    {
        public XSSPrevent()
        {
            Get("/Lesson0/XSSPrevent", Get);
        }

        public static Task Get(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "text/html";

            string content = @"
                <form method='get'>
                    Name: <input type='text' name='name'><br>
                          <input type='submit'>
                </form>";

            if (!string.IsNullOrWhiteSpace(request.Query["name"]))
            {
                string htmlEncode = WebUtility.HtmlEncode(request.Query["name"]);

                content += $@"<h1>Result from form: {htmlEncode}";
            }

            return response.WriteAsync(content);
        }
    }
}
