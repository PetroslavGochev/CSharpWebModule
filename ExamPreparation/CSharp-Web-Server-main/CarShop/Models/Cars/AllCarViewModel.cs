namespace CarShop.Models.Cars
{
    public class AllCarViewModel
    {
        public string Id { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public string PlateNumber { get; set; }

        public int FixedIssues { get; set; }

        public int RemainingIssues { get; set; }
    }
}
