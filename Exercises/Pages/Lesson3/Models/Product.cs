using System.ComponentModel.DataAnnotations;

namespace Exercises.Pages.Lesson3.Models
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; }
        [Required, MinLength(2), MaxLength(128)]
        public string Name { get; set; }
        
        [Required, Range(0, 99.99)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
    }
}
