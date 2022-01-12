using System;
using Dapper;
using Exercises.Pages.Lesson3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercises.Pages.Lesson3.Categories
{
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

            var connection = new DbUtils().GetDbConnection();
            string insert = @"INSERT INTO Category (Name) VALUES (@Name)";
            int numRowsEffected = connection.Execute(insert, Category);
            if (numRowsEffected != 1)
            {
                throw new Exception("INSERT Failed");
            }

            return RedirectToPage(nameof(Index));
        }
    }
}