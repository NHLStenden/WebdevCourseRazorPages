using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class PostAndQueryAndRoute : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {

        }

        public void OnPost([FromRoute] string action, [FromQuery]string queryStringValue, [FromForm] int productId)
        {
            Message = $"Action: {action}; QueryStringValue: {queryStringValue}; ProductId: {productId};";
        }
    }
}
