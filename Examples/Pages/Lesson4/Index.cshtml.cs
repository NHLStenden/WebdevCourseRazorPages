using System;
using System.Collections.Generic;
using Examples.Pages.Lesson3.Models;
using Examples.Pages.Lesson3.Repositories;
using Examples.Pages.Lesson4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson4
{
    public class Index : PageModel
    {
        private List<Category> _categories;

        [BindProperty(SupportsGet = true)]
        public Product Product { get; set; }

        public List<Category> Categories
        {
            get
            {
                _categories = new CategoryRepository().GetCategories();
                return _categories;
            }
            set => _categories = value;
        }

        public void OnGet()
        {
            Console.WriteLine("OnGet");
        }

        public JsonResult OnGetLoadProducts(string category = null)
        {
            //Filter based on category!
            return new JsonResult(ProductsRepository.GetProductWithCategories(category));
        }

        // public RedirectToPageResult OnPostDelete(int productId)
        // {
        //     bool deleted = ProductsRepository.DeleteProduct(productId);
        //
        //     TempData["ProductMessage"] = $"Product met productId {productId} is verwijderd";
        //
        //     return RedirectToPage("Index");
        // }

        public JsonResult OnPostDelete(int productId)
        {
            bool deleted = ProductsRepository.DeleteProduct(productId);
            return new JsonResult(deleted);
        }

        public IActionResult OnPostCreate()
        {
            if (Product.Price < 0)
            {
                ModelState.AddModelError("Product.Price", "Price is less than zero");
            }

            if (ModelState.IsValid)
            {
                var addProduct = ProductsRepository.AddProduct(Product);
                return new JsonResult(addProduct);
            }

            return BadRequest(ModelState);
        }

        public PartialViewResult OnGetCategoryNavigationPartial(string categoryNameFilter = null)
        {
            List<CategoryRepository.CategoryInfo> categoryInfos = new CategoryRepository().GetCategoryInfos();

            return Partial("_CategoryNav", new CategoryInfoViewModel()
            {
                CategoryNameFilter = categoryNameFilter,
                CategoryInfos = categoryInfos
            });
        }

        //Post Will Not work, with Remote Method (why I don't no, always the wrong page)!!!
        //See the PageRemote annotation in Product class
        public JsonResult OnGetCheckProductName()
        {
            bool productNameExists = ProductsRepository.ProductNameNotExists(Product.Name);

            return new JsonResult(productNameExists);
        }
    }
}
