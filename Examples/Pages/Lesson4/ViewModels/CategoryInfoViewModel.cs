using System.Collections.Generic;
using Examples.Pages.Lesson3.Repositories;

namespace Examples.Pages.Lesson4.ViewModels
{
    public class CategoryInfoViewModel
    {
        public string CategoryNameFilter { get; set; }
        public List<CategoryRepository.CategoryInfo> CategoryInfos { get; set; }
    }
}
