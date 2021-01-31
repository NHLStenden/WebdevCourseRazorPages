using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Exercises.Tests.Helpers
{
    public class TestHelpers
    {
        public static async Task<IHtmlDocument> RequestHtmlDocumentAsync(string requestUri)
        {
            var factory = new WebApplicationFactory<Startup>();

            var client = factory.CreateClient();
            var response = await client.GetAsync(requestUri);

            var document = await HtmlHelpers.GetDocumentAsync(response);
            return document;
        }

        public static async Task<IHtmlDocument> RequestHtmlDocumentWithPostAsync(IHtmlFormElement form, IHtmlButtonElement submitBtn)
        {
            var factory = new WebApplicationFactory<Startup>();

            var client = factory.CreateClient();
            var response = await client.SendAsync(form, submitBtn);

            var document = await HtmlHelpers.GetDocumentAsync(response);
            return document;
        }
    }
}
