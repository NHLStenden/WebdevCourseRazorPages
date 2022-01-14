using Exercises.Pages.Lesson3.Models;
using Exercises.Pages.Lesson3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercises.Pages.Lesson3.Categories;

public class Create : PageModel
{
    [BindProperty]
    public Category Category { get; set; }
    
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var createdCategory = new CategoryRepository().Add(Category);
        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return Redirect(nameof(Index));
    }
}