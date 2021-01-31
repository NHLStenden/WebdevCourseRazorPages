using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Examples.Pages.Lesson0
{
    public class PostRequest : CarterModule
    {
        public PostRequest()
        {
            Get("/Lesson0/PostRequest", Get);
            Post("/Lesson0/PostRequest", Post);
        }

        private static string CreateForm(string name, string email)
        {
            return $@"
                <form method='post'>
                    Name: <input type='text' name='name' value='{name}'><br>
                    E-mail: <input type='text' name='email' value='{email}'><br>
                    <input type='submit'>
                </form>";
        }

        public static Task Get(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "text/html";

            response.WriteAsync(
                CreateForm(string.Empty, string.Empty)
            );

            return response.CompleteAsync();
        }

        public static Task Post(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "text/html";

            string name = string.Empty;
            string email = string.Empty;

            name = request.Form["name"];
            email = request.Form["email"];

            string form = CreateForm(name, email);

            string displayResponse = $@"
                <h1>Results from post:<h1>
                <p>Name: {name} </p>
                <p>Email: {email} </p>
            ";

            response.WriteAsync(form + displayResponse);

            return response.CompleteAsync();
        }
    }
}
