using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Exercises.Pages.Lesson3.Models;

namespace Exercises.Pages.Lesson3.Repositories
{
    public class OrderRepository : IDisposable
    {
        private readonly IDbConnection _connection;

        public OrderRepository(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public List<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryByName(string categoryName)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts(int categoryId)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(int tableNr, int productId) {
            throw new NotImplementedException();
        }

        public void Pay(int tableNr)
        {
            throw new NotImplementedException();
        }

        public List<TableOrderViewModel> GetTableOrders(int tableNr)
        {
            throw new NotImplementedException();
        }

        public List<OrderLine> GetTableOrdersWithRelationships(int tableNr)
        {
            throw new NotImplementedException();

            // var x = _connection.Query<OrderLine, Product, Category, OrderLine>(
            //     @"SQL Query tovoegen
            //         ", (line, product, category) =>
            // {
            //     //vullen van line
            //     return line;
            // }, splitOn: "ProductId, CategoryId", param: new { TableNr = tableNr });
            // var res = x.ToList();
            // return res;
        }

        public class TableOrderViewModel
        {
            public int ProductId { get; set; }

            public string CategoryName { get; set; }

            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int Amount { get; set; }
            public decimal TotalPrice { get; set; }
        }
    }
}
