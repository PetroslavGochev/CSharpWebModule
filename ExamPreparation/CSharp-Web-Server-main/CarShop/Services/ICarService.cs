namespace CarShop.Services
{
    using CarShop.Models.Cars;
    using System.Collections.Generic;

    public interface ICarService
    {
        void AddCar(AddCarViewModel input);

        IEnumerable<AllCarViewModel> AllCar(string userId);

        CarViewModel CarIssues(string id);
    }
}
