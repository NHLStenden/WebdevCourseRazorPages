using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Exercises.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Exercises.Tests
{
    public class Lesson0 : IDisposable
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public Lesson0()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        public void Dispose()
        {
            _factory?.Dispose();
            _client?.Dispose();
        }

        private async Task<(HttpResponseMessage response, IHtmlDocument _document)> GETWebpageAsync(int assigmentNumber, string queryString = null)
        {
            string url = $"https://localhost:5001/Lesson0/assignment{assigmentNumber}";
            if (queryString != null && !string.IsNullOrWhiteSpace(queryString))
            {
                url += $"?{queryString}";
            }

            _client = _factory.CreateClient();
            var response = await _client.GetAsync(url);
            var document = await HtmlHelpers.GetDocumentAsync(response);
            return (response, document);
        }

        [Test]
        public async Task Assignment0()
        {
            var (response, document) = await GETWebpageAsync(0);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            document.ContentType.Should().Be("text/plain");
            document.Source.Text.Should().Be("Hello World");
        }

        [Test]
        public async Task Assignment1()
        {
            var (response, document) = await GETWebpageAsync(1);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            document.ContentType.Should().Be("text/html");
            document.Body.InnerHtml.Should().Be("<h1>Hello World</h1>");
        }

        [Test]
        public async Task Assignment2CorrectQueryParameter()
        {
            var (response, document) = await GETWebpageAsync(2,
                "name=test");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            document.ContentType.Should().Be("text/html");
            document.Body.InnerHtml.Should().Be("<h1>Hello test</h1>");
        }

        [Test]
        public async Task Assignment2MissingQueryParameter()
        {
            var (response, document) = await GETWebpageAsync(2);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            document.ContentType.Should().Be("text/html");
            document.Body.InnerHtml.Should().Be("<h1>Bad Request</h1>");
        }

        [Test]
        public async Task Assignment2InvalidQueryParameter()
        {
            var (response, document) = await GETWebpageAsync(2,
                "name=");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            document.ContentType.Should().Be("text/html");
            document.Body.InnerHtml.Should().Be("<h1>Bad Request</h1>");
        }

        [Test]
        public async Task Assignment3ValidQueryParameters()
        {
            var (response, document) = await GETWebpageAsync(3,
                "name=Piet&name=de Vries&age=32");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            document.ContentType.Should().Be("text/html");
            document.Body.InnerHtml.Should().Be("<ul>Leeftijd: 32 van<li>Piet</li><li>de Vries</li></ul>");
        }

        [Test]
        public async Task Assignment3InvalidAgeRequest()
        {
            var (response, document) = await GETWebpageAsync(3,
                "name=Piet&name=de Vries&age=");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            document.ContentType.Should().Be("text/html");
            document.Body.InnerHtml.Should().Be("<h1>Bad Request</h1>");
        }

        [Test]
        public async Task Assignment3InvalidAgeRequestNotAnNumber()
        {
            var (response, document) = await GETWebpageAsync(3,
                "name=Piet&name=de Vries&age=drie");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            document.ContentType.Should().Be("text/html");
            document.Body.InnerHtml.Should().Be("<h1>Bad Request</h1>");
        }

        [Test]
        public async Task Assignment4CheckNoScript()
        {
            var (response, document) = await GETWebpageAsync(4,
                "somevariable=%3Cscript%3Ealert(%27script%20from%20querystring%27);%3C/script%3E");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            document.ContentType.Should().Be("text/html");
            document.Source.Text.Should().NotBe("<script>alert('script from querystring');</script>");
        }

        [Test]
        public async Task Assignment5CheckForm()
        {
            var (response, document) = await GETWebpageAsync(5);

            var htmlFormElement = document.Forms[0];

            htmlFormElement.Method.ToUpper().Should().Be("POST");

            var firstNameInput = htmlFormElement.QuerySelector("input[name='firstname']");
            firstNameInput.SetAttribute("value", "Jan");

            var lastNameInput = htmlFormElement.QuerySelector("input[name='lastname']");
            lastNameInput.SetAttribute("value", "de Vries");

            var ageInput = htmlFormElement.QuerySelector("input[name='age']");
            ageInput.SetAttribute("value", "20");

            //var result = document.Forms[0].SubmitAsync().Result;

            var httpResponseMessage = _factory.CreateClient().SendAsync((IHtmlFormElement)htmlFormElement, (IHtmlButtonElement)
                htmlFormElement.QuerySelector("button[type='submit']")).Result;

            HtmlHelpers.GetDocumentAsync(httpResponseMessage).Result.Source.Text.Should()
                .Be("<h1>de Vries, Jan is 20 jaren oud</h1>");
        }


        [Test]
        [TestCase("", "e", "aa20")]
        [TestCase("e", "", "20.0")]
        [TestCase("e", "", "")]
        public async Task Assignment5CheckIncorrectInput(string firstNameInputValue, string lastNameInputValue, string ageInputValue)
        {
            var (response, document) = await GETWebpageAsync(5);

            var htmlFormElement = document.Forms[0];

            htmlFormElement.Method.ToUpper().Should().Be("POST");

            var firstNameInput = htmlFormElement.QuerySelector("input[name='firstname']");
            firstNameInput.SetAttribute("value", firstNameInputValue);

            var lastNameInput = htmlFormElement.QuerySelector("input[name='lastname']");
            lastNameInput.SetAttribute("value", lastNameInputValue);

            var ageInput = htmlFormElement.QuerySelector("input[name='age']");
            ageInput.SetAttribute("value", ageInputValue);

            //var result = document.Forms[0].SubmitAsync().Result;

            var postDocument = PostDocument(htmlFormElement,
                htmlFormElement.QuerySelector("button[type='submit']"));

            postDocument.QuerySelector("input[name='firstname']").NextSibling.Text().ToLower().Should()
                .Contain("ongeldige input");
            postDocument.QuerySelector("input[name='lastname']").NextSibling.Text().ToLower().Should()
                .Contain("ongeldige input");
            postDocument.QuerySelector("input[name='age']").NextSibling.Text().ToLower().Should()
                .Contain("ongeldige input");

            postDocument.QuerySelector("input[name='firstname']").Attributes["value"].Value.Should()
                .Be(firstNameInputValue);
            postDocument.QuerySelector("input[name='lastname']").Attributes["value"].Value.Should()
                .Be(lastNameInputValue);
            postDocument.QuerySelector("input[name='age']").Attributes["value"].Value.Should()
                .Be(ageInputValue);
        }

        private IHtmlDocument PostDocument(IHtmlFormElement htmlFormElement, IElement htmlButtonElement)
        {
            var httpResponseMessage = _factory.CreateClient().SendAsync((IHtmlFormElement) htmlFormElement,
                (IHtmlButtonElement) htmlButtonElement).Result;

            var postDocument = HtmlHelpers.GetDocumentAsync(httpResponseMessage).Result;
            return postDocument;
        }

        [Test]
        public async Task Assignment5CheckFormOutputScriptEncoded()
        {
            var (response, document) = await GETWebpageAsync(5);

            document.Forms[0].Method.ToUpper().Should()
                .Be("POST");

            var firstNameInput = document.Forms[0].QuerySelector("input[name='firstname']");
            firstNameInput.SetAttribute("value", "<script>alert('test');</script>");

            var lastNameInput = document.Forms[0].QuerySelector("input[name='lastname']");
            lastNameInput.SetAttribute("value", "<script>alert('test');</script>");

            var ageInput = document.Forms[0].QuerySelector("input[name='age']");
            ageInput.SetAttribute("value", "<script>alert('test');</script>");

            //var result = document.Forms[0].SubmitAsync().Result;

            var httpResponseMessage = _factory.CreateClient().SendAsync((IHtmlFormElement)document.Forms[0], (IHtmlButtonElement)
                document.Forms[0].QuerySelector("button[type='submit']")).Result;

            var postDocument = HtmlHelpers.GetDocumentAsync(httpResponseMessage).Result;

            string result = HtmlHelpers.GetDocumentAsync(httpResponseMessage).Result.Source.Text;
            result.Should()
                .NotContain("<script>alert('test');</script>");

            result.Should()
                .Contain("&lt;script&gt;alert(&#39;test&#39;);&lt;/script&gt;");
        }
    }
}
