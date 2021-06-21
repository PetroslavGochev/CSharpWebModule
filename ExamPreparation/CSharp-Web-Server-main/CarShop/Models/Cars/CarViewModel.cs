using CarShop.Models.Issues;
using System.Collections.Generic;

namespace CarShop.Models.Cars
{
    public class CarViewModel
    {
        public string Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public IEnumerable<IssueViewModel> Issues { get; set; }
    }
}
