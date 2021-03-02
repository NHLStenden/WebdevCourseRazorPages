using System.Collections.Generic;
using Examples.Pages.Lesson2.Models;
using Examples.Pages.Lesson2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson2.Products
{
    public class Index : PageModel
    {
        private readonly ProductsRepository _productRepository;
        public IEnumerable<Product> Products { get; set; }

        public Index()
        {
            _productRepository = new ProductsRepository();
        }

        public void OnGet()
        {
            Products = _productRepository.GetProductsInShop();
        }

        public void OnPostDelete(int productId)
        {
            Products = _productRepository.DeleteProduct(productId);
        }
    }
}
