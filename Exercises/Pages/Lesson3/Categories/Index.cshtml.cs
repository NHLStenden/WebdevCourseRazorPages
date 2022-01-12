using System.Collections.Generic;
using System.Linq;
using Dapper;
using Exercises.Pages.Lesson3.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercises.Pages.Lesson3.Categories
{
    public class Index : PageModel
    {
        public IEnumerable<Category> Categories { get; set; }
        
        public void OnGet()
        {
            var connection = new DbUtils().GetDbConnection();
            string sql = "SELECT * FROM Category";
            Categories = connection.Query<Category>(sql);
        }
    }
}