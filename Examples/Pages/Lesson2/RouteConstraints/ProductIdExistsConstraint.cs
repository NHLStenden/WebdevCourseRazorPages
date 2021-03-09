using System.Globalization;
using Examples.Pages.Lesson2.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Examples.Pages.Lesson2.RouteConstraints
{
    public class ProductIdExistsConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var value = values["productid"];
            if (value == null)
                return false;

            int productId;
            if (int.TryParse(value.ToString(), out productId))
            {
                var productsRepository = new ProductsRepository();
                var product = productsRepository.GetProductById(productId);
                return product != null;
            }
            else
            {
                return false;
            }
        }
    }
}
