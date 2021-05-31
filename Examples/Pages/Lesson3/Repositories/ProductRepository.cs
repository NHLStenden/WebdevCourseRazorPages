using System.Collections.Generic;
using System.Linq;
using Dapper;
using Examples.Pages.Lesson3.Models;

namespace Examples.Pages.Lesson3.Repositories
{
    public static class ProductsRepository
    {
        public static Product GetProductById(int id)
        {
            using var db = DbUtils.GetDbConnection();
            var products =
                db.QueryFirst<Product>("SELECT * FROM Product WHERE ProductId = @ProductId",
                    new {ProductId = id});
            return products;
        }

        public static List<Product> GetProducts()
        {
            using var db = DbUtils.GetDbConnection();
            var products =
                db.Query<Product>("SELECT * FROM Product")
                    .ToList();
            return products;
        }

        public static List<Product> GetProductWithCategories(string category)
        {
            //https://www.learndapper.com/relationships
            string sql =
                @"SELECT p.ProductId, p.Name, p.Description, p.Price, p.SalePrice, p.CategoryId, c.CategoryId, c.Name 
                            FROM Product p JOIN Category c 
                                on c.CategoryId = p.CategoryId WHERE @Category IS NULL OR c.Name = @Category";

            using var db = DbUtils.GetDbConnection();
            var products = db.Query<Product, Category, Product>(sql, (product, category) =>
            {
                product.Category = category;
                return product;
            }, splitOn: "CategoryId", param: new {Category = category}).ToList();

            return products;
        }

        public static bool DeleteProduct(int productId)
        {
            using var db = DbUtils.GetDbConnection();
            var result = db.Execute("DELETE FROM Product WHERE ProductId = @ProductId", new
            {
                ProductId = productId
            });
            return result == 1;
        }

        public static Product AddProduct(Product product)
        {
            using var db = DbUtils.GetDbConnection();
            int newProductId = db.ExecuteScalar<int>(
                @"INSERT INTO Product (Name, Description, Price, SalePrice, CategoryId) 
                    VALUES (@Name, @Description, @Price, @SalePrice, @CategoryId); 
                    SELECT LAST_INSERT_ID();", new
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    SalePrice = product.SalePrice,
                    CategoryId = product.CategoryId
                });
            product.ProductId = newProductId;
            product.Category = new CategoryRepository().GetCategoryById(product.CategoryId);

            return product;
        }

        public static bool ProductNameNotExists(string productName)
        {
            if (!string.IsNullOrWhiteSpace(productName))
            {
                productName = productName.Trim().ToLower();
            }

            using var db = DbUtils.GetDbConnection();
            int rowCount = db.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM Product WHERE Name = @Name", 
                new { Name = productName}
            );

            return rowCount < 1;
        }

    }
}
