using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLStendenCafe.Models;
using NHLStendenCafe.Repositories;

namespace NHLStendenCafe.Pages.Categories;

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