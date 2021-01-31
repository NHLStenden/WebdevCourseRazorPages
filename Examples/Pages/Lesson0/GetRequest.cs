using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Examples.Pages.Lesson0
{
    public class GetRequest : CarterModule
    {
        public GetRequest()
        {
            Get("/Lesson0/GetRequest", Get);
        }

        public static Task Get(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "text/html";

            string content = "<h1>Hello World</h1>";

            return response.WriteAsync(content);
        }




    }
}
