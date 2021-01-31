using System.Net;
using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Examples.Pages.Lesson0
{
    public class GetRequestWithParametersFromForm : CarterModule
    {
        public GetRequestWithParametersFromForm()
        {
            Get("/Lesson0/GetRequestWithParametersFromForm", Get);
        }

        private Task Get(HttpRequest request, HttpResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.OK;
            response.ContentType = "text/html";

            string firstName = request.Query["firstName"];
            string lastName = request.Query["lastName"];

            string displayInput = $@"
                <h1>Voornaam: {firstName}</h1>
                <h1>Achternaam: {lastName}</h1>";

            string form = $@"
                <form>
                    <input name=""firstName"" type=""text"" value=""{firstName}""><br/>
                    <input name=""lastName"" type=""text"" value=""{lastName}""><br/>
                    <button type=""submit"">Verzenden</button> 
                </form>
            ";

            response.WriteAsync(displayInput + form);

            return response.CompleteAsync();
        }
    }
}
