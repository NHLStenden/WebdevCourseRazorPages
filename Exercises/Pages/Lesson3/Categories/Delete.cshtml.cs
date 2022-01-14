using Exercises.Pages.Lesson3.Models;
using Exercises.Pages.Lesson3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercises.Pages.Lesson3.Categories;

public class Delete : PageModel
{
    public Category Category { get; set; }
    
    public void OnGet([FromRoute] int categoryId)
    {
        Category = new CategoryRepository().Get(categoryId);
    }

    public IActionResult OnPostDelete([FromRoute]int categoryId)
    {
        bool success = new CategoryRepository().Delete(categoryId);
        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}