using System.Collections.Generic;
using Examples.Pages.Lesson3.Repositories;
using Examples.Pages.Lesson4.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Examples.Pages.Lesson4.ViewComponents
{
    public class CategoryInfoComponent4 : ViewComponent
    {
        public IViewComponentResult Invoke(string categoryNameFilter = null)
        {
            ICollection<CategoryRepository.CategoryInfo> categoryInfos = CategoryRepository.GetCategoryInfos();

            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(new CategoryInfoViewModel()
            {
                CategoryNameFilter = categoryNameFilter,
                CategoryInfos = categoryInfos
            });
        }
    }
}
