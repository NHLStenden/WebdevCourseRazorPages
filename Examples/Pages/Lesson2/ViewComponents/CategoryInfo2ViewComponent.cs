using System.Collections.Generic;
using System.Linq;
using Examples.Pages.Lesson2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Examples.Pages.Lesson2.ViewComponents
{
    public class CategorySummary
    {
        public string CategoryName { get; set; }
        public int NumberOfProducts { get; set; }
    }
    public class CategoryInfo2ViewComponent : ViewComponent
    {
        private ProductsRepository _productsRepository;

        public CategoryInfo2ViewComponent()
        {
            _productsRepository = new ProductsRepository();
        }

        public IViewComponentResult Invoke(string categoryName = null)
        {
            var products = _productsRepository.GetProductsInShop()
                .OrderBy(x => x.Name)
                .ToList();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                products = products.Where(x => x.Category.Name == categoryName).ToList();
            }

            List<CategorySummary> categorySummaries = new List<CategorySummary>();
            foreach (var productByGroup in products
                .GroupBy(x => x.Category))
            {
                categorySummaries.Add(new CategorySummary()
                {
                    CategoryName = productByGroup.Key.Name,
                    NumberOfProducts = productByGroup.Count()
                });
            }

            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(categorySummaries);
        }
    }
}