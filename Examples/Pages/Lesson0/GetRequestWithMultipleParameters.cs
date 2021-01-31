using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Examples.Pages.Lesson0
{
    public class GetRequestWithMultipleParameters : CarterModule
    {
        public GetRequestWithMultipleParameters()
        {
            Get("/Lesson0/GetRequestWithMultipleParameters", Get);
        }

        public  static Task Get(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "text/html";

            string queryString = request.QueryString.Value;
            string firstname = request.Query["firstname"];
            if (string.IsNullOrWhiteSpace(firstname))
            {
                firstname = "Please enter a value for firstname";
            }

            string lastname = request.Query["lastname"];
            if (string.IsNullOrWhiteSpace(lastname))
            {
                lastname = "Please enter a value for lastname";
            }

            string content = $@"
                <h1>queryString: {queryString}</h1>
                <h1>firstname: {firstname}</h1>
                <h1>lastname: {lastname}</h1>
            ";

            return response.WriteAsync(content);
        }
    }
}
