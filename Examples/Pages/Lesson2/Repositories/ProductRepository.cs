using System;
using System.Collections.Generic;
using System.Linq;
using Examples.Pages.Lesson2.Models;

namespace Examples.Pages.Lesson2.Repositories
{
    //Todo: split ProductsRepository into two repo's (Category & Product) 
    public class ProductsRepository
    {
        private static List<Product> Products { get; set; }
        private static List<Category> Categories { get; set; }

        private static ProductsRepository Instance = null;

        public ProductsRepository()
        {
            if (Instance == null)
            {
                SeedData();
            }

            Instance = this;
        }

        private void SeedData()
        {
            Products = new List<Product>();

            Random random = new Random(122323);

            decimal minDiscount = 0.1m;
            decimal maxDiscount = 0.5m;

            Categories = new List<Category>();
            for (int i = 1; i < 4; i++)
            {
                Categories.Add(new Category()
                {
                    CategoryId = i,
                    Name = $"Category {i}"
                });
            }

            for (int i = 1; i <= 10; i++)
            {
                int randomCategoryIndex = random.Next(0, Categories.Count);

                var product = new Product()
                {
                    ProductId = i,
                    Name = $"Product {i}",
                    Description = $"Product {i} Description",
                    Price = 10 * i,
                    InShopDate = DateTime.Today,
                    Category = Categories[randomCategoryIndex]
                };

                var discount = (decimal)random.NextDouble() * (maxDiscount - minDiscount) + minDiscount;
                if (i % 3 == 0)
                {
                    product.SalePrice = Math.Round(product.Price * (1.0m - discount));
                }

                Products.Add(product);
            }
        }

        public List<Product> GetProductsInShop()
        {
            return Products.Where(x => DateTime.Today <= x.InShopDate).ToList();
        }

        public List<Product> DeleteProduct(int productId)
        {
            Products = Products.Where(x => x.ProductId != productId).ToList();
            return Products;
        }

        public List<Product> AddProduct(Product product)
        {
            int newProductId = Products.Max(x => x.ProductId) + 1;

            product.ProductId = newProductId;

            product.Category = Categories.First(x => x.CategoryId == product.CategoryId);

            Products.Add(product);
            return Products;
        }

        public List<Category> GetCategories()
        {
            return Categories.OrderBy(x => x.Name).ToList();
        }

        public Product GetProductById(int productId)
        {
            return Products.FirstOrDefault(x => x.ProductId == productId);
        }
    }
}
