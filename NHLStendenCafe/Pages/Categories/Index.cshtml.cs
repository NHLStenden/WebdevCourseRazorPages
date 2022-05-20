using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLStendenCafe.Models;
using NHLStendenCafe.Repositories;

namespace NHLStendenCafe.Pages.Categories;

public class Index : PageModel
{
    public IEnumerable<Category> Categories { get; set; }
    
    public void OnGet()
    {
        Categories = new CategoryRepository().Get();
    }
}