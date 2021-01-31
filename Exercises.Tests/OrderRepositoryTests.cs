using System.Collections.Generic;
using System.IO;
using Dapper;
using Exercises.Pages.Lesson3.Models;
using Exercises.Pages.Lesson3.Repositories;
using FluentAssertions;
using NUnit.Framework;

namespace Exercises.Tests
{
    public class OrderRepositoryTests
    {
        [SetUp]
        public void CreateAndSeedDatabase()
        {
            using var connection = DbUtils.GetDbConnection();
            var mySQLCafeScriptPath =
                Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
                    "MySQLCafe.SQL");

            var mySQLCafeScript = File.ReadAllText(mySQLCafeScriptPath);
            connection.Execute(mySQLCafeScript);
        }

        [Test]
        public void GetCategories_ShouldReturnListOfCategoriesOrderByName()
        {
            var sut = new OrderRepository(DbUtils.GetDbConnection());

            var categories = sut.GetCategories();

            string[] names = {"Bier", "Frisdranken", "Speciaal bier", "Warme dranken", "Wijnen en aperitieven"};
            categories.Should()
                .Equal(names, (category, name) => category.Name == name);
            categories.Should()
                .OnlyHaveUniqueItems(x => x.CategoryId)
                .And
                .OnlyContain(x => x.CategoryId > 0);
        }

        [Test]
        public void GetCategoryByName_ShouldReturnBierCategory()
        {
            var sut = new OrderRepository(DbUtils.GetDbConnection());

            var category = sut.GetCategoryByName("Bier");

            category.Name.Should().Be("Bier");
        }

        [Test]
        public void GetProductsByCategoryId_ShouldReturnListOfProductFromCategoryOrderedByName()
        {
            var sut = new OrderRepository(DbUtils.GetDbConnection());
            var bierCategory = sut.GetCategoryByName("Bier");
            var bierCatId = bierCategory.CategoryId;

            var products = sut.GetProducts(bierCategory.CategoryId);

            var expectedProducts = new[]
            {
                new Product {Name = "Dommelsch 0.22", Price = 2.30m},
                new Product {Name = "Dommelsch 0.25", Price = 2.50m},
                new Product {Name = "Dommelsch 0.50", Price = 4.50m},
                new Product {Name = "Jupiler N/A 0.0%", Price = 2.50m}
            };
            products.Should().Equal(expectedProducts, (product, expectedProduct) =>
                product.Name == expectedProduct.Name &&
                product.Price == expectedProduct.Price &&
                product.CategoryId == bierCatId
            );
        }

        [Test]
        public void GetTableOrders_ShouldReturnListOfTableOrders()
        {
            var sut = new OrderRepository(DbUtils.GetDbConnection());

            var orders = sut.GetTableOrders(1);

            orders.Should().BeEquivalentTo(new List<OrderRepository.TableOrderViewModel>
            {
                new OrderRepository.TableOrderViewModel
                {
                    ProductId = 3,
                    CategoryName = "Bier",
                    ProductName = "Dommelsch 0.50",
                    Price = 4.50m,
                    Amount = 2,
                    TotalPrice = 9.00m
                },
                new OrderRepository.TableOrderViewModel
                {
                    ProductId = 15,
                    CategoryName = "Speciaal bier",
                    ProductName = "Biestheuvel blond 6%",
                    Price = 4.00m,
                    Amount = 3,
                    TotalPrice = 12.00m
                },
                new OrderRepository.TableOrderViewModel
                {
                    ProductId = 44,
                    CategoryName = "Warme dranken",
                    ProductName = "Cappuccino",
                    Price = 2.50m,
                    Amount = 3,
                    TotalPrice = 7.50m
                },
                new OrderRepository.TableOrderViewModel
                {
                    ProductId = 45,
                    CategoryName = "Warme dranken",
                    ProductName = "Latte Macchiato",
                    Price = 2.50m,
                    Amount = 2,
                    TotalPrice = 5.00m
                },
                new OrderRepository.TableOrderViewModel
                {
                    ProductId = 49,
                    CategoryName = "Warme dranken",
                    ProductName = "Warme chocomel met slagroom",
                    Price = 3.50m,
                    Amount = 4,
                    TotalPrice = 14.00m
                },
                new OrderRepository.TableOrderViewModel
                {
                    ProductId = 36,
                    CategoryName = "Wijnen en aperitieven",
                    ProductName = "Huiswijnen Rood",
                    Price = 3.75m,
                    Amount = 4,
                    TotalPrice = 15.00m
                }
            });
        }

        [Test]
        public void GetTableOrdersWithRelationships_ShouldReturnListOfOrderLines()
        {
            var sut = new OrderRepository(DbUtils.GetDbConnection());

            var orderLines = sut.GetTableOrdersWithRelationships(1);

            orderLines.Should().BeEquivalentTo(new List<OrderLine>
            {
                new OrderLine
                {
                    TableNr = 1,
                    ProductId = 3,
                    Amount = 2,
                    Product = new Product
                    {
                        ProductId = 3,
                        Name = "Dommelsch 0.50",
                        CategoryId = 2,
                        Price = 4.50m,
                        Category = new Category
                        {
                            CategoryId = 2,
                            Name = "Bier"
                        }
                    },
                    CostObject = null
                },
                new OrderLine
                {
                    TableNr = 1,
                    ProductId = 15,
                    Amount = 3,
                    Product = new Product
                    {
                        ProductId = 15,
                        Name = "Biestheuvel blond 6%",
                        CategoryId = 5,
                        Price = 4.00m,
                        Category = new Category
                        {
                            CategoryId = 5,
                            Name = "Speciaal bier"
                        }
                    },
                    CostObject = null
                },
                new OrderLine
                {
                    TableNr = 1,
                    ProductId = 44,
                    Amount = 3,
                    Product = new Product
                    {
                        ProductId = 44,
                        Name = "Cappuccino",
                        CategoryId = 4,
                        Price = 2.50m,
                        Category = new Category
                        {
                            CategoryId = 4,
                            Name = "Warme dranken"
                        }
                    },
                    CostObject = null
                },
                new OrderLine
                {
                    TableNr = 1,
                    ProductId = 45,
                    Amount = 2,
                    Product = new Product
                    {
                        ProductId = 45,
                        Name = "Latte Macchiato",
                        CategoryId = 4,
                        Price = 2.50m,
                        Category = new Category
                        {
                            CategoryId = 4,
                            Name = "Warme dranken"
                        }
                    },
                    CostObject = null
                },
                new OrderLine
                {
                    TableNr = 1,
                    ProductId = 49,
                    Amount = 4,
                    Product = new Product
                    {
                        ProductId = 49,
                        Name = "Warme chocomel met slagroom",
                        CategoryId = 4,
                        Price = 3.50m,
                        Category = new Category
                        {
                            CategoryId = 4,
                            Name = "Warme dranken"
                        }
                    },
                    CostObject = null
                },
                new OrderLine
                {
                    TableNr = 1,
                    ProductId = 36,
                    Amount = 4,
                    Product = new Product
                    {
                        ProductId = 36,
                        Name = "Huiswijnen Rood",
                        CategoryId = 3,
                        Price = 3.75m,
                        Category = new Category
                        {
                            CategoryId = 3,
                            Name = "Wijnen en aperitieven"
                        }
                    },
                    CostObject = null
                }
            });
        }

        [Test]
        public void AddOrder_ShouldAddNewOrderLine()
        {
            var sut = new OrderRepository(DbUtils.GetDbConnection());

            int beforeAddOrderCount = DbUtils.GetDbConnection()
                .QuerySingle<int>("SELECT COUNT(1) FROM OrderLine WHERE TableNr = @TableNr AND ProductId = @ProductId",
                    param: new {TableNr = 1, ProductId = 20});

            beforeAddOrderCount.Should().Be(0);

            sut.AddOrder(1, 20);

            OrderLine orderLine = DbUtils.GetDbConnection()
                .QuerySingle<OrderLine>("SELECT * FROM OrderLine WHERE TableNr = @TableNr AND ProductId = @ProductId",
                    param: new {TableNr = 1, ProductId = 20});

            orderLine.Should().BeEquivalentTo(new OrderLine
            {
                TableNr = 1,
                ProductId = 20,
                Amount = 1,
                Product = null,
                CostObject = null
            });
        }

        [Test]
        public void AddOrder_ShouldUpdateExistingOrderLine()
        {
            var sut = new OrderRepository(DbUtils.GetDbConnection());

            OrderLine existingOrderToUpdate = DbUtils.GetDbConnection()
                .QuerySingle<OrderLine>("SELECT * FROM OrderLine WHERE TableNr = @TableNr AND ProductId = @ProductId",
                    param: new {TableNr = 1, ProductId = 15});

            sut.AddOrder(1, 15);

            OrderLine orderLine = DbUtils.GetDbConnection()
                .QuerySingle<OrderLine>("SELECT * FROM OrderLine WHERE TableNr = @TableNr AND ProductId = @ProductId",
                    param: new {TableNr = 1, ProductId = 15});

            existingOrderToUpdate.Amount += 1;

            orderLine.Should().BeEquivalentTo(existingOrderToUpdate);
        }

        [Test]
        public void Pay_ShouldDeleteAllOrderLines()
        {
            var sut = new OrderRepository(DbUtils.GetDbConnection());

            var rowCount = DbUtils.GetDbConnection()
                .QuerySingle<int>("SELECT Count(1) FROM OrderLine WHERE TableNr = @TableNr",
                    new {TableNr = 1});

            rowCount.Should().Be(6);

            sut.Pay(1);

            var rowCountAfterPay = DbUtils.GetDbConnection()
                .QuerySingle<int>("SELECT Count(1) FROM OrderLine WHERE TableNr = @TableNr",
                    new {TableNr = 1});

            rowCountAfterPay.Should().Be(0);
        }
    }
}
