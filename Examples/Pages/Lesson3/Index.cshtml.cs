using System.Collections.Generic;
using Examples.Pages.Lesson3.Models;
using Examples.Pages.Lesson3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson3
{
    public class Index : PageModel
    {
        public List<Product> Products { get; set; }

        public void OnGet(string category)
        {
            //Filter based on category!
            Products = ProductsRepository.GetProductWithCategories(category);

        }

        public RedirectToPageResult OnPostDelete(int productId)
        {
            bool deleted = ProductsRepository.DeleteProduct(productId);

            if (deleted)
            {
                TempData["ProductMessage"] = $"Product met productId {productId} is verwijderd";
            }
            else
            {
                //zou in principe niet moeten gebeuren!
                TempData["ProductMessage"] = $"Product met productId {productId} is niet verwijderd";
            }


            return RedirectToPage("Index");
        }

        public PageResult OnPostCreate(Product product)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return Page();
        }
    }
}
