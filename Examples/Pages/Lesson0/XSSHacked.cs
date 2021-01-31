using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Examples.Pages.Lesson0
{
    public class XSSHacked : CarterModule
    {
        public XSSHacked()
        {
            Get("/Lesson0/XSSHacked", Get);
        }

        //Submit the following value in the textbox (name):
        //<script type="text/javascript">alert("text");</script>

        //Submit the following value in the textbox (name):
        //<h1><b>Test</b></h1>

        //this is called a XSS attack, google it for a explanation
        //very dangerous, to prevent see PreventXSS.cs
        //Get("/Lesson1/XSS", HackedExample);

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
                content += $@"<h1>Result from form: {request.Query["name"]}";
            }

            return response.WriteAsync(content);
        }
    }
}
