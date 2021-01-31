using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Exercises.Pages.Lesson3.Models;

namespace Exercises.Pages.Lesson3.Repositories
{
    public class CostObjectRepository : IDisposable
    {
        private readonly IDbConnection _connection;

        public CostObjectRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public List<CostObject> GetAll()
        {
            throw new NotImplementedException();
        }

        public CostObject Get(int costObjectId)
        {
            throw new NotImplementedException();
        }

        public CostObject Add(CostObject costObject)
        {
            throw new NotImplementedException();

            // CostObject result = null; //haal hier je net ingevoegd cost object op uit de database;
            // return result;
        }

        // * CostObject Delete(int costObjectId)
        public bool Delete(int costObjectId)
        {
            throw new NotImplementedException();
        }

        public CostObject Update(CostObject costObject)
        {
            throw new NotImplementedException();

            // CostObject result = null; //haal hier je net geupdate cost object op uit de database;
            // return result;
        }



        public List<OrderLine> GetOrderLines(int costObjectId)
        {
            throw new NotImplementedException();
            // var orderlines = _connection
            //     .Query<OrderLine, CostObject, Product, Category, OrderLine>(
            //         @"Voeg hier je SQL Toe"
            //         , (orderLine, costObject, product, category) =>
            //         {
            //             //magie toevoegen om orderLine en gerelateerde producten te vullen
            //             return orderLine;
            //         },
            //         new {costObjectId},
            //         splitOn: "CostObjectId, ProductId, CategoryId").ToList();
            // return orderlines;
        }

        public class BillLine
        {
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public decimal ProductPrice { get; set; }
            public int ProductOrderedAmount { get; set; }
            public decimal TotalProductPrice { get; set; }
        }

        public List<BillLine> GetBillLines(int costObjectId)
        {
            throw new NotImplementedException();
        }

        public List<BillLine> GetBillLinesFromOrderLines(int costObjectId)
        {
            List<OrderLine> orderLines = GetOrderLines(costObjectId);

            List<BillLine> result = new List<BillLine>();
            foreach (var orderLine in orderLines)
            {
                result.Add(new BillLine()
                {
                    CategoryName = orderLine.Product.Category.Name,
                    ProductName = orderLine.Product.Name,
                    ProductPrice = orderLine.Product.Price,
                    ProductOrderedAmount = orderLine.Amount,
                    TotalProductPrice = orderLine.Amount * orderLine.Product.Price
                });
            }

            return result;
        }

        public class TotalPrice
        {
            public decimal Total { get; set; }
            public string CostObjectName { get; set; }
        }

        public TotalPrice GetTotalBillPrice(int costObjectId)
        {
            throw new NotImplementedException();
        }

        public TotalPrice GetTotalBillPriceMultiMapping(int costObjectId)
        {
            throw new NotImplementedException();
            // var totalPrice = new TotalPrice();
            // var result = _connection.Query<CostObject, OrderLine, Product, TotalPrice>(
            //     @"SQL Query toevoegen",
            //     map: (costObject, orderLine, product) =>
            //     {
            //         //vullen/updaten van totalPrice
            //         return totalPrice;
            //     }, new {costObjectId},
            //     splitOn: "ProductId, CostObjectId");
            // // de volgorde van de splitOn columns kan soms van invloed zijn. Werkt het niet verande dan de volgorde!
            // return totalPrice;
        }


    }
}
