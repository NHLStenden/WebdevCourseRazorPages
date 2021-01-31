using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapper;
using Exercises.Pages.Lesson3.Models;
using Exercises.Pages.Lesson3.Repositories;
using FluentAssertions;
using NUnit.Framework;

namespace Exercises.Tests
{
    public class CostObjectRepositoryTests
    {
        [SetUp]
        public void CreateAndSeedDatabase()
        {
            using var connection = DbUtils.GetDbConnection();
            var mySQLCafeScriptPath = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "MySQLCafe.SQL");

            string mySQLCafeScript = File.ReadAllText(mySQLCafeScriptPath);
            connection.Execute(mySQLCafeScript);
        }

        [Test]
        public void GetAll_ShouldReturnListOfCostObjectsOrderByName()
        {
            using CostObjectRepository sut = new CostObjectRepository(DbUtils.GetDbConnection());

            List<CostObject> costObjects = sut.GetAll();

            List<CostObject> expectation = new List<CostObject>()
            {
                new CostObject() {Name = "HBO-ICT Kerst Borrel", Budget = 100, Email = "info@hbo-ict.com", TelNr = "058-2131313"},
                new CostObject() {Name = "HBO-ICT Uitje", Budget = 100, Email = "info@hbo-ict.com", TelNr = "058-2131313"},
                new CostObject() { Budget = 1000.00M, CostObjectId = 3, Email = "info@hbo-ict.com", Name = "HBO-ICT Viering 100 jarig bestaan", TelNr = "058-2131313"}
            }.OrderBy(x => x.Name).ToList();
            costObjects.Should().BeEquivalentTo(expectation, options =>
                options.Excluding(o => o.CostObjectId)
            );
        }

        [Test]
        public void Add_ShouldAddCostObject()
        {
            using CostObjectRepository sut = new CostObjectRepository(DbUtils.GetDbConnection());

            var objectToAdd = new CostObject()
                {Name = "Borrel 1", Budget = 120, Email = "borrel@borrel.com", TelNr = "058-1111112"};
            var addedObject = sut.Add(objectToAdd);


            addedObject.CostObjectId.Should().BeGreaterThan(2);
            var expectedCostObject = new CostObject()
                {Name = "Borrel 1", Budget = 120, Email = "borrel@borrel.com", TelNr = "058-1111112"};
            addedObject.Should().BeEquivalentTo(
                expectedCostObject,
                options => options.Excluding(x => x.CostObjectId)
            );
        }

        [Test]
        public void Get_ShouldRetrieveCostObjectById()
        {
            using CostObjectRepository sut = new CostObjectRepository(DbUtils.GetDbConnection());

            var expected = new CostObject()
                {Name = "HBO-ICT Uitje", Budget = 100, Email = "info@hbo-ict.com", TelNr = "058-2131313"};

            var costObject = sut.Get(2);
            costObject.Should().BeEquivalentTo(
                expected,
                options => options.Excluding(x => x.CostObjectId)
            );
        }

        [Test]
        public void Delete_ShouldThrowFKException()
        {
            using CostObjectRepository sut = new CostObjectRepository(DbUtils.GetDbConnection());

            sut.Invoking(_ => sut.Delete(1))
                .Should().Throw<MySqlConnector.MySqlException>();
        }

        [Test]
        public void Delete_ShouldDeleteCostObject()
        {
            using CostObjectRepository sut = new CostObjectRepository(DbUtils.GetDbConnection());

            bool success = sut.Delete(3);

            success.Should().BeTrue();

            var deleteCostObject = sut.Get(3);
            deleteCostObject.Should().BeNull();
        }

        [Test]
        public void Update_ShouldUpdateCostObject()
        {
            using CostObjectRepository sut = new CostObjectRepository(DbUtils.GetDbConnection());
            var costObject = sut.Get(1);
            costObject.Budget = 1;
            costObject.Email = "t@t.com";
            costObject.Name = "Test Update";
            costObject.TelNr = "058-2121111";

            var updatedCostObject = sut.Update(costObject);

            var expectedCostObject = new CostObject()
            {
                Budget = 1, Email = "t@t.com", Name = "Test Update", TelNr = "058-2121111"
            };
            updatedCostObject.Should().BeEquivalentTo(expectedCostObject, options =>
                options.Excluding(x => x.CostObjectId));
            updatedCostObject.Should().BeEquivalentTo(sut.Get(1));
        }

        [Test]
        public void GetBillLines_ShouldDisplayBillDetails()
        {
            using CostObjectRepository sut = new CostObjectRepository(DbUtils.GetDbConnection());
            List<CostObjectRepository.BillLine> billLines = sut.GetBillLines(1);

            var expectation = new List<CostObjectRepository.BillLine>
            {
                new CostObjectRepository.BillLine
                {
                    ProductName = "Cappuccino",
                    CategoryName = "Warme dranken",
                    ProductPrice = 2.50m,
                    ProductOrderedAmount = 3,
                    TotalProductPrice = 7.50m
                },
                new CostObjectRepository.BillLine
                {
                    ProductName = "Koffie verkeerd",
                    CategoryName = "Warme dranken",
                    ProductPrice = 2.50m,
                    ProductOrderedAmount = 4,
                    TotalProductPrice = 10.00m
                },
                new CostObjectRepository.BillLine
                {
                    ProductName = "Latte Macchiato",
                    CategoryName = "Warme dranken",
                    ProductPrice = 2.50m,
                    ProductOrderedAmount = 2,
                    TotalProductPrice = 5.00m
                },
                new CostObjectRepository.BillLine
                {
                    ProductName = "Thee (Lipton)",
                    CategoryName = "Warme dranken",
                    ProductPrice = 2.30m,
                    ProductOrderedAmount = 4,
                    TotalProductPrice = 9.20m
                },
                new CostObjectRepository.BillLine
                {
                    ProductName = "Warme chocomel met slagroom",
                    CategoryName = "Warme dranken",
                    ProductPrice = 3.50m,
                    ProductOrderedAmount = 4,
                    TotalProductPrice = 14.00m
                }
            };

            billLines.Should().BeEquivalentTo(expectation);
        }

        [Test]
        public void GetBillLinesFromOrderLines_ShouldDisplayBillDetails()
        {
            using CostObjectRepository sut = new CostObjectRepository(DbUtils.GetDbConnection());
            List<CostObjectRepository.BillLine> billLines = sut.GetBillLinesFromOrderLines(1);

            var expectation = new List<CostObjectRepository.BillLine>
            {
                new CostObjectRepository.BillLine
                {
                    ProductName = "Cappuccino",
                    CategoryName = "Warme dranken",
                    ProductPrice = 2.50m,
                    ProductOrderedAmount = 3,
                    TotalProductPrice = 7.50m
                },
                new CostObjectRepository.BillLine
                {
                    ProductName = "Koffie verkeerd",
                    CategoryName = "Warme dranken",
                    ProductPrice = 2.50m,
                    ProductOrderedAmount = 4,
                    TotalProductPrice = 10.00m
                },
                new CostObjectRepository.BillLine
                {
                    ProductName = "Latte Macchiato",
                    CategoryName = "Warme dranken",
                    ProductPrice = 2.50m,
                    ProductOrderedAmount = 2,
                    TotalProductPrice = 5.00m
                },
                new CostObjectRepository.BillLine
                {
                    ProductName = "Thee (Lipton)",
                    CategoryName = "Warme dranken",
                    ProductPrice = 2.30m,
                    ProductOrderedAmount = 4,
                    TotalProductPrice = 9.20m
                },
                new CostObjectRepository.BillLine
                {
                    ProductName = "Warme chocomel met slagroom",
                    CategoryName = "Warme dranken",
                    ProductPrice = 3.50m,
                    ProductOrderedAmount = 4,
                    TotalProductPrice = 14.00m
                }
            };

            billLines.Should().BeEquivalentTo(expectation);
        }


        [Test]
        public void GetOrderLines_ByCostObjectId_ShouldListOrderLines()
        {
            using CostObjectRepository sut = new CostObjectRepository(DbUtils.GetDbConnection());
            List<OrderLine> orderLines = sut.GetOrderLines(1);

            var listOrderLine = new List<OrderLine>
            {
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
                CostObject = new CostObject
                {
                  CostObjectId = 1,
                  Name = "HBO-ICT Kerst Borrel",
                  Budget = 100.00m,
                  Email = "info@hbo-ict.com",
                  TelNr = "058-2131313"
                }
              },
              new OrderLine
              {
                TableNr = 2,
                ProductId = 46,
                Amount = 4,
                Product = new Product
                {
                  ProductId = 46,
                  Name = "Koffie verkeerd",
                  CategoryId = 4,
                  Price = 2.50m,
                  Category = new Category
                  {
                    CategoryId = 4,
                    Name = "Warme dranken"
                  }
                },
                CostObject = new CostObject
                {
                  CostObjectId = 1,
                  Name = "HBO-ICT Kerst Borrel",
                  Budget = 100.00m,
                  Email = "info@hbo-ict.com",
                  TelNr = "058-2131313"
                }
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
                CostObject = new CostObject
                {
                  CostObjectId = 1,
                  Name = "HBO-ICT Kerst Borrel",
                  Budget = 100.00m,
                  Email = "info@hbo-ict.com",
                  TelNr = "058-2131313"
                }
              },
              new OrderLine
              {
                TableNr = 2,
                ProductId = 43,
                Amount = 4,
                Product = new Product
                {
                  ProductId = 43,
                  Name = "Thee (Lipton)",
                  CategoryId = 4,
                  Price = 2.30m,
                  Category = new Category
                  {
                    CategoryId = 4,
                    Name = "Warme dranken"
                  }
                },
                CostObject = new CostObject
                {
                  CostObjectId = 1,
                  Name = "HBO-ICT Kerst Borrel",
                  Budget = 100.00m,
                  Email = "info@hbo-ict.com",
                  TelNr = "058-2131313"
                }
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
                CostObject = new CostObject
                {
                  CostObjectId = 1,
                  Name = "HBO-ICT Kerst Borrel",
                  Budget = 100.00m,
                  Email = "info@hbo-ict.com",
                  TelNr = "058-2131313"
                }
              }
            };

            orderLines.Should().BeEquivalentTo(listOrderLine);
        }

        public class TotalPriceExpectation
        {
            public int CostObjectId { get; set; }
            public CostObjectRepository.TotalPrice TotalPrice { get; set; }
        }
        public static IEnumerable<TotalPriceExpectation> TotalPricesExpectations
        {
            get
            {
                yield return new TotalPriceExpectation {CostObjectId = 1, TotalPrice = new CostObjectRepository.TotalPrice()
                    {Total = 45.70m, CostObjectName = "HBO-ICT Kerst Borrel"}};
                yield return new TotalPriceExpectation {CostObjectId = 2, TotalPrice = new CostObjectRepository.TotalPrice()
                    {Total = 70.00m, CostObjectName = "HBO-ICT Uitje"}};
            }
        }

        [Test]
        [TestCaseSource(nameof(TotalPricesExpectations))]
        public void GetTotalBill_ShouldDisplayTotalBillPrice(TotalPriceExpectation totalPriceExpectation)
        {
            using CostObjectRepository sut = new CostObjectRepository(DbUtils.GetDbConnection());

            var totalPrice = sut.GetTotalBillPrice(totalPriceExpectation.CostObjectId);

            totalPrice.Should().BeEquivalentTo(totalPriceExpectation.TotalPrice);
        }

        [Test]
        [TestCaseSource(nameof(TotalPricesExpectations))]
        public void GetTotalBillPriceMultiMapping_ShouldDisplayTotalBillPrice(TotalPriceExpectation totalPriceExpectation)
        {
            using CostObjectRepository sut = new CostObjectRepository(DbUtils.GetDbConnection());

            var totalPrice = sut.GetTotalBillPriceMultiMapping(totalPriceExpectation.CostObjectId);

            totalPrice.Should().BeEquivalentTo(totalPriceExpectation.TotalPrice);
        }
    }
}
