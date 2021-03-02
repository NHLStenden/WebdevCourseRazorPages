using System.Collections.Generic;
using Examples.Pages.Lesson3.Models;
using Examples.Pages.Lesson3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson3
{
    public class Create : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }

        [TempData]
        public string ProductMessage { get; set; }

        public List<Category> Categories { get; set; }

        public void OnGet()
        {
            Categories = new CategoryRepository().GetCategories();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var addProduct = ProductsRepository.AddProduct(Product);

            ProductMessage = $"Het volgende product is toegevoegd : {addProduct.Name}";

            return RedirectToPage("Index");
        }



    }
}
