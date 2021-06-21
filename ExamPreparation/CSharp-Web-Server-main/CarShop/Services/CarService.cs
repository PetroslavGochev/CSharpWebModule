namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Models.Cars;
    using CarShop.Models.Issues;
    using System.Collections.Generic;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly CarShopDbContext db;

        public CarService(CarShopDbContext db)
        {
            this.db = db;
        }

        public void AddCar(AddCarViewModel input)
        {
            var car = new Car()
            {
                Model = input.Model,
                Year = input.Year,
                PictureUrl = input.Image,
                PlateNumber = input.PlateNumber,
                OwnerId = input.OwnerId,
                Owner = this.db.Users.FirstOrDefault(u => u.Id == input.OwnerId),
            };

            this.db.Cars.Add(car);

            this.db.SaveChanges();
        }

        public IEnumerable<AllCarViewModel> AllCar(string userId)
        {
            var user = this.db.Users.FirstOrDefault(u => u.Id == userId);

            var cars = this.db.Cars
                .Where(c => !user.IsMechanic ? c.OwnerId == user.Id : true)
                .ToArray();

            var carsCollection = new List<AllCarViewModel>();
            foreach (var car in cars)
            {
                var issues = this.db.Issues
                    .Where(i => i.CarId == car.Id)
                    .ToList();
                var carViewModel = new AllCarViewModel()
                {
                    Id = car.Id,
                    Model = car.Model,
                    PlateNumber = car.PlateNumber,
                    ImageUrl = car.PictureUrl,
                    FixedIssues = issues.Where(i => i.IsFixed).Count(),
                    RemainingIssues = issues.Where(i => !i.IsFixed).Count(),
                };

                carsCollection.Add(carViewModel);
            }

            return carsCollection;
        }

        public CarViewModel CarIssues(string id)
        {
            var car = this.db.Cars.FirstOrDefault(c => c.Id == id);
            var issues = this.db.Issues.Where(i => i.CarId == car.Id).ToArray();

            var issuesViewModel = new List<IssueViewModel>();
            foreach (var issue in issues)
            {
                var issueViewModel = new IssueViewModel()
                {
                    Id = issue.Id,
                    Description = issue.Description,
                    IsFixed = issue.IsFixed,
                };
                issuesViewModel.Add(issueViewModel);
            }

            var carViewModel = new CarViewModel()
            {
                Id = car.Id,
                Model = car.Model,
                Year = car.Year,
                Issues = issuesViewModel,
            };

            return carViewModel;
        }
    }
}
