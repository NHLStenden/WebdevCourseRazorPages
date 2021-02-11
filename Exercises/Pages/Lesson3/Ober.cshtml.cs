using System.Collections.Generic;
using Exercises.Pages.Lesson3.Models;
using Exercises.Pages.Lesson3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercises.Pages.Lesson3
{
    public class Ober : PageModel
    {
        private OrderRepository _orderRepository;

        [BindProperty]
        public int? TableNr { get; set; }

        [BindProperty]
        public int? CategoryId { get; set; }

        [BindProperty]
        public int? ProductId { get; set; }

        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

        public List<OrderRepository.TableOrderViewModel> TableOrders { get; set; }

        public List<OrderLine> TableOrdersWithRelationship { get; set; }

        public Ober()
        {
            _orderRepository = new OrderRepository(new DbUtils().GetDbConnection());
        }

        private void LoadData()
        {
            Categories = _orderRepository.GetCategories();

            if (CategoryId.HasValue)
            {
                Products = _orderRepository.GetProducts(CategoryId.Value);
            }

            if (TableNr.HasValue)
            {
                TableOrdersWithRelationship = _orderRepository.GetTableOrdersWithRelationships(TableNr.Value);

                TableOrders = _orderRepository.GetTableOrders(TableNr.Value);
            }
        }



        public void OnGet()
        {
            LoadData();
        }

        public void OnPostSelectTable()
        {
            LoadData();
        }

        public void OnPostSelectCategory()
        {
            LoadData();
        }

        public void OnPostAddProductToOrder()
        {
            _orderRepository.AddOrder(TableNr.Value, ProductId.Value);

            LoadData();
        }

        public void OnPostPay()
        {
            _orderRepository.Pay(TableNr.Value);

            LoadData();
        }

    }
}
