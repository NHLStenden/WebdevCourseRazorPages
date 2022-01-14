using System.ComponentModel.DataAnnotations;

namespace Exercises.Pages.Lesson3.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        [Required, MinLength(2), MaxLength(128)]
        public string Name { get; set; }
    }
}
