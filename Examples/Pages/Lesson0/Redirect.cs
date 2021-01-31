using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Examples.Pages.Lesson0
{
    public class Redirect : CarterModule
    {
        public Redirect()
        {
            Get("/Lesson0/Redirect", Get);
        }

        public static Task Get(HttpRequest request, HttpResponse response)
        {
            //!!!Let op een redirect werkt niet in het live preview (iframe)!!! https://stackoverflow.com/questions/26252166/redirect-url-is-not-working-with-iframe
            //!!!Let op een redirect werkt niet in het live preview (iframe)!!! https://stackoverflow.com/questions/26252166/redirect-url-is-not-working-with-iframe
            //!!!Let op een redirect werkt niet in het live preview (iframe)!!! https://stackoverflow.com/questions/26252166/redirect-url-is-not-working-with-iframe

            //take a look at the Network tab in ChromeDev tools

            // HTTP/1.1 302 Found
            // Date: Tue, 17 Nov 2020 13:18:38 GMT
            // Server: Kestrel
            // Content-Length: 0
            // Location: http://www.google.com

            response.Redirect("http://www.google.com");
            return response.CompleteAsync();
        }
    }
}
