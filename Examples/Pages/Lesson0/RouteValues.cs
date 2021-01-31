using System;
using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Examples.Pages.Lesson0
{
    public class RouteValues : CarterModule
    {
        public RouteValues()
        {
            Get("/Lesson0/WithOneRouteValue/{id:int}", WithOneRouteValue);
            Get("/Lesson0/WithTwoRouteValue/{category}/{subcategory}", WithTwoRouteValue);
        }

        public static Task WithOneRouteValue(HttpRequest request, HttpResponse response)
        {
            int id = Int32.Parse(request.RouteValues["id"].ToString());
            return response.WriteAsync($"<h1>Id: {id} </h1>");
        }

        public static Task WithTwoRouteValue(HttpRequest request, HttpResponse response)
        {
            string category = request.RouteValues["category"].ToString();
            string subCategory = request.RouteValues["subcategory"].ToString();
            return response.WriteAsync($"<h1>category: {category} subcategory: {subCategory}</h1>");
        }

        public static Task WithMultipleRouteValue(HttpRequest request, HttpResponse response)
        {
            response.WriteAsync("RouteValues: ");

            response.WriteAsync("<ul>");
            foreach (var requestRouteValue in request.RouteValues)
            {
                response.WriteAsync($"<li>Key: {requestRouteValue.Key} Value: {requestRouteValue.Value}</li>");
            }
            response.WriteAsync("</ul>");

            return response.CompleteAsync();
        }
    }
}
