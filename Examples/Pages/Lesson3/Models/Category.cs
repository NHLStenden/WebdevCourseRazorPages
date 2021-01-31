using System.ComponentModel.DataAnnotations;

namespace Examples.Pages.Lesson3.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MinLength(2), MaxLength(128)]
        public string Name { get; set; }
    }
}
