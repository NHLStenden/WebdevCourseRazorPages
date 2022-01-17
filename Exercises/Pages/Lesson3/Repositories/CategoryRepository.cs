using System.Collections.Generic;
using System.Data;
using Dapper;
using Exercises.Pages.Lesson3.Models;

namespace Exercises.Pages.Lesson3.Repositories
{
    public class CategoryRepository
    {
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        public Category Get(int categoryId)
        {
            string sql = "SELECT * FROM Category WHERE CategoryId = @categoryId";
            
            using var connection = GetConnection();
            var category = connection.QuerySingle<Category>(sql, new { categoryId });
            return category;
        }

        public IEnumerable<Category> Get()
        {
            string sql = "SELECT * FROM Category ORDER BY Name";
            
            using var connection = GetConnection();
            var categories = connection.Query<Category>(sql);
            return categories;
        }

        public Category Add(Category category)
        {
            string sql = @"
                INSERT INTO Category (Name) 
                VALUES (@Name); 
                SELECT * FROM Category WHERE CategoryId = LAST_INSERT_ID()";
            
            using var connection = GetConnection();
            var addedCategory = connection.QuerySingle<Category>(sql, category);
            return addedCategory;
        }

        public bool Delete(int categoryId)
        {
            string sql = @"DELETE FROM Category WHERE CategoryId = @categoryId";
            
            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { categoryId });
            return numOfEffectedRows == 1;
        }

        public Category Update(Category category)
        {
            string sql = @"
                UPDATE Category SET 
                    Name = @Name 
                WHERE CategoryId = @CategoryId;
                SELECT * FROM Category WHERE CategoryId = @CategoryId";
            
            using var connection = GetConnection();
            var updatedCategory = connection.QuerySingle<Category>(sql, category);
            return updatedCategory;
        }
    }
}