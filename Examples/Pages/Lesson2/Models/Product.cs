using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Examples.Pages.Lesson2.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Naam mag niet leeg zijn"), MinLength(2), MaxLength(12),
         Display(Name = "Naam", Prompt = "Geef een geldige product naam op")]
        public string Name { get; set; }
        [MaxLength(128)]
        [DefaultValue("Geen beschrijving aanwezig")]
        public string Description { get; set; }
        [Required, Range(0, 10000)]
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }

        //[DefaultValue(typeof(DateTime), DateTime.Now)]
        //https://www.learnrazorpages.com/razor-pages/forms/dates-and-times#:~:text=The%20default%20value%20for%20a,00%3A00%20in%20the%20control.
        [DataType(DataType.Date)]
        public DateTime InShopDate { get; set; } = DateTime.Today;

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
