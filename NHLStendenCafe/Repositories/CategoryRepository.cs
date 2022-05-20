using System.Data;
using Dapper;
using NHLStendenCafe.Models;

namespace NHLStendenCafe.Repositories
{
    public class CategoryRepository
    {

        public Category Get(int categoryId)
        {
            string sql = "SELECT * FROM Category WHERE CategoryId = @categoryId";
            
            using var connection = DbUtils.GetDbConnection();
            var category = connection.QuerySingle<Category>(sql, new { categoryId });
            return category;
        }

        public IEnumerable<Category> Get()
        {
            string sql = "SELECT * FROM Category ORDER BY Name";
            
            using var connection = DbUtils.GetDbConnection();
            var categories = connection.Query<Category>(sql);
            return categories;
        }

        public Category Add(Category category)
        {
            string sql = @"
                INSERT INTO Category (Name) 
                VALUES (@Name); 
                SELECT * FROM Category WHERE CategoryId = LAST_INSERT_ID()";
            
            using var connection = DbUtils.GetDbConnection();
            var addedCategory = connection.QuerySingle<Category>(sql, category);
            return addedCategory;
        }

        public bool Delete(int categoryId)
        {
            string sql = @"DELETE FROM Category WHERE CategoryId = @categoryId";
            
            using var connection = DbUtils.GetDbConnection();
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
            
            using var connection = DbUtils.GetDbConnection();
            var updatedCategory = connection.QuerySingle<Category>(sql, category);
            return updatedCategory;
        }
    }
}