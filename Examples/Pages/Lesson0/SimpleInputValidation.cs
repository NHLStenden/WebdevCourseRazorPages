using System;
using System.Linq;
using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Examples.Pages.Lesson0
{
    public class SimpleInputValidation : CarterModule
    {
        public SimpleInputValidation()
        {
            Get("/Lesson0/SimpleInputValidation", GetSimpleInputValidation);
        }

        public static Task GetSimpleInputValidation(HttpRequest request, HttpResponse response)
        {
            response.ContentType = "text/html";
            response.StatusCode = StatusCodes.Status200OK;

            string name = "";
            string nameError = "";

            if (request.Query.ContainsKey("firstName"))
            {
                name = request.Query["firstName"].First();
                nameError = string.IsNullOrWhiteSpace(name) ? "Name is required" : string.Empty;
                if (name.Length < 3)
                {
                    nameError = "Name should be at least 2 characters";
                }
            }
            else
            {
                nameError = "Name is required";
            }


            string ageStr = request.Query.ContainsKey("age") ? request.Query["age"].First() : "";
            int age = 0;
            string ageError = string.IsNullOrWhiteSpace(ageStr) || !int.TryParse(ageStr, out age)
                ? "Age is required and should be an int" : string.Empty;

            string minPriceStr = request.Query.ContainsKey("minKey") ? request.Query["minPrice"].First() : "";
            string maxPriceStr = request.Query.ContainsKey("maxPrice") ? request.Query["maxPrice"].First() : "";
            string fromDateStr = request.Query.ContainsKey("fromDate") ? request.Query["fromDate"].First() : "";

            decimal minPrice = decimal.MinValue;
            string minPriceError =
                string.IsNullOrWhiteSpace(minPriceStr) || !decimal.TryParse(minPriceStr, out minPrice)
                    ? "Min Price Error"
                    : "";

            if (minPrice < 1.0m)
            {
                minPriceError = "Min Price is to low";
            }

            decimal maxPrice = decimal.MaxValue;
            string maxPriceError =
                string.IsNullOrWhiteSpace(maxPriceStr) || !decimal.TryParse(maxPriceStr, out maxPrice)
                    ? "Max Price Error"
                    : "";



            DateTime fromDate = DateTime.MinValue;
            string fromDateError =
                string.IsNullOrWhiteSpace(fromDateStr) || !DateTime.TryParse(fromDateStr, out fromDate)
                    ? "From Date Error"
                    : "";



            string content = $@"
                name: {name} {nameError} <br>
                age: {age} {ageError} <br>
                minPrice: {minPrice} {minPriceError} <br>
                maxPrice: {maxPrice} {maxPriceError} <br>
                fromDate: {fromDate} {fromDateError} <br>
            ";

            return response.WriteAsync(content);
        }
    }
}
