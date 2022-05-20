using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLStendenCafe.Models;
using NHLStendenCafe.Repositories;

namespace NHLStendenCafe.Pages.Categories;

public class Update : PageModel
{
    public Category Category { get; set; }
    
    public void OnGet([FromQuery]int categoryId)
    {
        Category = new CategoryRepository().Get(categoryId);
    }

    public IActionResult OnPost(Category category)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var updatedCategory = new CategoryRepository().Update(category);

        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}