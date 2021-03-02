using System.Collections.Generic;
using Examples.Pages.Lesson3.Repositories;
using Examples.Pages.Lesson4.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Examples.Pages.Lesson4.ViewComponents
{
    public class CategoryInfo4ViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string categoryNameFilter = null)
        {
            List<CategoryRepository.CategoryInfo> categoryInfos = new CategoryRepository().GetCategoryInfos(categoryNameFilter);

            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(new CategoryInfoViewModel()
            {
                CategoryNameFilter = categoryNameFilter,
                CategoryInfos = categoryInfos
            });
        }
    }
}
