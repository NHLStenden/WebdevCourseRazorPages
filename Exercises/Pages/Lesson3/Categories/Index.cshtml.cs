using System.Collections;
using System.Collections.Generic;
using Exercises.Pages.Lesson3.Models;
using Exercises.Pages.Lesson3.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercises.Pages.Lesson3.Categories;

public class Index : PageModel
{
    public IEnumerable<Category> Categories { get; set; }
    
    public void OnGet()
    {
        Categories = new CategoryRepository().Get();
    }
}