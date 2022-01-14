using System.Collections.Generic;
using System.Data;
using Dapper;
using Exercises.Pages.Lesson3.Models;

namespace Exercises.Pages.Lesson3.Repositories
{
    public class ProductRepository
    {
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        public Product Get(int productId)
        {
            string sql = "SELECT * FROM Product WHERE ProductId = @productId";
            
            using var connection = GetConnection();
            var product = connection.QuerySingle<Product>(sql, new {productId});
            return product;
        }

        public IEnumerable<Product> Get()
        {
            string sql = "SELECT * FROM Product ORDER BY Name, Price";
            
            using var connection = GetConnection();
            var products = connection.Query<Product>(sql);
            return products;
        }

        public Product Add(Product product)
        {
            string sql = @"
                INSERT INTO Product (Name, CategoryId, Price) 
                VALUES (@Name, @CategoryId, @Price); 
                SELECT * FROM Product WHERE ProductId = LAST_INSERT_ID()";
            
            using var connection = GetConnection();
            var addedProduct = connection.QuerySingle<Product>(sql);
            return addedProduct;
        }

        public bool Delete(int productId)
        {
            string sql = @"DELETE FROM Product WHERE ProductId = @productId";
            
            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { productId });
            return numOfEffectedRows == 1;
        }

        public Product Update(Product product)
        {
            string sql = @"
                UPDATE Product SET 
                    Name = @Name, CategoryId = @CategoryId, @Price = @Price 
                WHERE @ProductId = @ProductId;
                SELECT * FROM Product WHERE ProductId = @ProductId";
            
            using var connection = GetConnection();
            var addedProduct = connection.QuerySingle<Product>(sql);
            return addedProduct;
        }
    }
}