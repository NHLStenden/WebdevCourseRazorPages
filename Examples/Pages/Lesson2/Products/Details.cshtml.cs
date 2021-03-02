using Examples.Pages.Lesson2.Models;
using Examples.Pages.Lesson2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson2.Products
{
    public class Details : PageModel
    {


        [BindProperty]
        public Product Product { get; set; }


        public void OnGet(int productId)
        {
            var productsRepository = new ProductsRepository();
            Product = productsRepository.GetProductById(productId);
        }
    }
}
