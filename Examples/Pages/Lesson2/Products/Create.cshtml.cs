using System.Collections.Generic;
using Examples.Pages.Lesson2.Models;
using Examples.Pages.Lesson2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson2.Products
{
    public class Create : PageModel
    {
        private readonly ProductsRepository _productRepository;

        [BindProperty]
        public Product Product { get; set; }

        public List<Category> Categories { get; set; }

        public Create()
        {
            _productRepository = new ProductsRepository();
        }

        public void OnGet()
        {
            Categories = _productRepository.GetCategories();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            
            _productRepository.AddProduct(Product);

            return RedirectToPage("Index");
        }
    }
}
