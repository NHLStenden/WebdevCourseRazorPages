using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Lesson1 : IDisposable
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public Lesson1()
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
            string url = $"https://localhost:5001/Lesson1/Assignment{assigmentNumber}";
            if (queryString != null && !string.IsNullOrWhiteSpace(queryString))
            {
                url += $"?{queryString}";
            }

            _client = _factory.CreateClient();
            var response = await _client.GetAsync(url);
            var document = await HtmlHelpers.GetDocumentAsync(response);
            return (response, document);
        }

        private async Task<(HttpResponseMessage response, IHtmlDocument _document)> GETWebpageFromUrAsync(string url)
        {
            _client = _factory.CreateClient();
            var response = await _client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return (response, null);
            }
            var document = await HtmlHelpers.GetDocumentAsync(response);
            return (response, document);
        }

        private async Task<(HttpResponseMessage response, IHtmlDocument _document)> Post(IHtmlFormElement form, IHtmlButtonElement btn)
        {
            var response = await _client.SendAsync(form, btn);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return (response, null);
            }

            var document = await HtmlHelpers.GetDocumentAsync(response);
            return (response, document);
        }


        [Test]
        public async Task Assignment1()
        {
            var (response, document) = await GETWebpageAsync(1);

            var scoreHome = int.Parse(document.QuerySelector("#scoreHome").Text());
            var scoreAway = int.Parse(document.QuerySelector("#scoreAway").Text());

            scoreHome.Should().Be(0);
            scoreAway.Should().Be(0);

            await ClickDirection("#incrementHome", 1, 0);
            await ClickDirection("#incrementHome", 2, 0);
            await ClickDirection("#incrementHome", 3, 0);

            await ClickDirection("#incrementAway", 3, 1);
            await ClickDirection("#incrementAway", 3, 2);
            await ClickDirection("#incrementAway", 3, 3);

            await ClickDirection("#decrementHome", 2, 3);
            await ClickDirection("#decrementAway", 2, 2);

            await ClickDirection("#reset", 0, 0);

            async Task ClickDirection(string idSelector, int homeScore, int awayScore)
            {
                var button = (IHtmlAnchorElement) document.QuerySelector(idSelector);
                (_, document) = await GETWebpageFromUrAsync(button.Href);

                int home = int.Parse(document.QuerySelector("#scoreHome").Text());
                int away = int.Parse(document.QuerySelector("#scoreAway").Text());

                home.Should().Be(homeScore);
                away.Should().Be(awayScore);
            }
        }

        [Test]
        public async Task Assignment2()
        {
            var (response, document) = await GETWebpageAsync(2);

            var buttonTexts = document.QuerySelectorAll("a").Select(x => x.TextContent);
            buttonTexts.Should().BeEquivalentTo(new string[] {"Clear", "Left", "Right", "Forward", "Backward"});

            var directions = new List<string>() {"Left"};
            await ClickDirection("Left");

            directions.Add("Right");
            await ClickDirection("Right");

            directions.Add("Forward");
            await ClickDirection("Forward");

            directions.Add("Backward");
            await ClickDirection("Backward");

            directions.Clear();
            await ClickDirection("Clear");

            async Task ClickDirection(string direction)
            {
                var leftButton = (IHtmlAnchorElement) document.QuerySelector($"a:contains('{direction}')");
                (_, document) = await GETWebpageFromUrAsync(leftButton.Href);
                var result = document.QuerySelectorAll("#route>li").Select(x => x.Text());
                result.Should().BeEquivalentTo(directions);
            }
        }

        [Test]
        public async Task Assignment3()
        {
            var (response, document) = await GETWebpageAsync(3);

            await Calculate("#addBtn", 10, 10);
            await Calculate("#addBtn", 10, 20);
            await Calculate("#subBtn", 5, 15);
            await Calculate("#mulBtn", 2, 30);
            await Calculate("#divBtn", 3, 10);

            //BadRequest (delen door nul)
            await Calculate("#divBtn", 0, 10);

            async Task Calculate(string buttonSelector, decimal input, decimal resultOfCalculation)
            {
                var inputElement = (IHtmlInputElement) document.QuerySelector("#input");
                inputElement.Value = input.ToString();

                var formElement = (IHtmlFormElement) document.QuerySelector("#calculatorForm");
                var buttonElement = (IHtmlButtonElement) document.QuerySelector(buttonSelector);

                (response, document) = await Post(formElement, buttonElement);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    responseText.Should().Be("Delen door nul is niet toegestaan");
                }
                else
                {
                    decimal result = decimal.Parse(document.QuerySelector("#result").TextContent);
                    result.Should().Be(resultOfCalculation);
                }
            }
        }

        [Test]
        public async Task Assignment4Correct()
        {
            var (_, document) = await GETWebpageFromUrAsync("https://localhost:5001/Lesson1/Assignment4/cat1/subcat2/3");

            string category = document.QuerySelector("#categoryHeading").Text();
            category.Should().Contain("cat1");

            string subCategory = document.QuerySelector("#subCategoryHeading").Text();
            subCategory.Should().Contain("subcat2");

            string productId = document.QuerySelector("#productIdHeading").Text();
            productId.Should().Contain("3");

            (_, document) = await GETWebpageFromUrAsync("https://localhost:5001/Lesson1/Assignment4/cat1/subcat2");

            category = document.QuerySelector("#categoryHeading").Text();
            category.Should().Contain("cat1");

            subCategory = document.QuerySelector("#subCategoryHeading").Text();
            subCategory.Should().Contain("subcat2");

            productId = document.QuerySelector("#productIdHeading").Text();
            productId.Should().Contain("Geen productId");

            (_, document) = await GETWebpageFromUrAsync("https://localhost:5001/Lesson1/Assignment4/cat1");

            category = document.QuerySelector("#categoryHeading").Text();
            category.Should().Contain("cat1");

            subCategory = document.QuerySelector("#subCategoryHeading").Text();
            subCategory.Should().Contain("Geen subcategory");

            productId = document.QuerySelector("#productIdHeading").Text();
            productId.Should().Contain("Geen productId");
        }

        [Test]
        public async Task Assignment4InvalidProductId()
        {
            var (response, document) = await GETWebpageFromUrAsync("https://localhost:5001/Lesson1/Assignment4/cat1/subCat1/-3");
            document.Should().BeNull();

            (_, document) = await GETWebpageFromUrAsync("https://localhost:5001/Lesson1/Assignment4/cat1/subCat3/0");
            document.Should().BeNull();

            (_, document) = await GETWebpageFromUrAsync("https://localhost:5001/Lesson1/Assignment4/cat1/catSub/1");
            document.Should().BeNull();

            (_, document) = await GETWebpageFromUrAsync("https://localhost:5001/Lesson1/Assignment4/cat1/catSub100WW/2");
            document.Should().BeNull();
        }

        [Test]
        public async Task Assignment5CookieObject()
        {
            var (response, document) = await GETWebpageAsync(5);

            await ClickButton("#btnHappy", @"MoodCounter={""Happy"":1,""Disappointed"":0,""Angry"":0}");
            await ClickButton("#btnHappy", @"MoodCounter={""Happy"":2,""Disappointed"":0,""Angry"":0}");

            await ClickButton("#btnDisappointed", @"MoodCounter={""Happy"":2,""Disappointed"":1,""Angry"":0}");
            await ClickButton("#btnDisappointed", @"MoodCounter={""Happy"":2,""Disappointed"":2,""Angry"":0}");

            await ClickButton("#btnDeleteCookie", @"MoodCounter=");

            await ClickButton("#btnAngry", @"MoodCounter={""Happy"":0,""Disappointed"":0,""Angry"":1}");
            await ClickButton("#btnAngry", @"MoodCounter={""Happy"":0,""Disappointed"":0,""Angry"":2}");

            async Task ClickButton(string buttonSelector, string expectedCookie)
            {
                var button = (IHtmlButtonElement) document.QuerySelector(buttonSelector);
                (response, document) = await Post((IHtmlFormElement) document.QuerySelector("form"), button);

                WebUtility.UrlDecode(response.Headers.GetValues("Set-Cookie").First().Split(';')[0])
                    .Should().Be(expectedCookie);
            }
        }
    }
}
