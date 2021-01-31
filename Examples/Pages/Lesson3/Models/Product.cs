using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Examples.Pages.Lesson3.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        // [PageRemote(
        //     ErrorMessage ="Product name already exists",
        //     AdditionalFields = "__RequestVerificationToken",
        //     HttpMethod ="GET", //POST will not work, posting to the wrong page even with PageName set correct!
        //     PageHandler ="CheckProductName"
        // )]
        [Required, MinLength(2), MaxLength(12)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        public decimal? SalePrice { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [BindProperty]
        public Category Category { get; set; }
    }
}
