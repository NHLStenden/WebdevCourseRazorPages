using System.Collections.Generic;
using Exercises.Pages.Lesson3.Models;
using Exercises.Pages.Lesson3.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercises.Pages.Lesson3
{
    public class Overview : PageModel
    {
        public ICollection<CostObject> CostObjects { get; set; }
        public List<CostObjectRepository.BillLine> BillLines { get; set; } = null;
        public CostObjectRepository.TotalPrice TotalPrice { get; set; }

        public int? CostObjectId { get; set; }
        public void OnGet(int? costObjectId)
        {
            CostObjectId = costObjectId;

            var costObjectRepository = new CostObjectRepository(new DbUtils().GetDbConnection());
            CostObjects = costObjectRepository.GetAll();

            if (costObjectId.HasValue)
            {
                BillLines = costObjectRepository.GetBillLines(costObjectId.Value);
                TotalPrice = costObjectRepository.GetTotalBillPrice(costObjectId.Value);
            }
        }


    }
}
