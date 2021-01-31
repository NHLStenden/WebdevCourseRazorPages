using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Examples.Pages.Lesson0
{
    public class GetRequestWithOneParameter : CarterModule
    {
        public GetRequestWithOneParameter()
        {
            Get("/Lesson0/GetRequestWithOneParameter", Get);
        }

        public  static Task Get(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "text/html";

            string queryString = request.QueryString.Value;
            string name = request.Query["name"];
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Please enter a value for name";
            }

            string content = $@"
                <h1>queryString: {queryString}</h1>
                <h1>name: {name}</h1>
            ";

            return response.WriteAsync(content);
        }
    }
}
