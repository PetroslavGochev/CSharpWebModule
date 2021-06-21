namespace CarShop.Controllers
{
    using CarShop.Models.Cars;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    [Authorize]
    public class CarsController : Controller
    {
        private const string Client = "Sorry only client can add a car";

        private readonly IUserService userService;
        private readonly IValidator validator;
        private readonly ICarService carService;

        public CarsController(
            IUserService userService,
            IValidator validator,
            ICarService carService)
        {
            this.userService = userService;
            this.validator = validator;
            this.carService = carService;
        }
        public HttpResponse All()
        {
            var cars = this.carService.AllCar(this.User.Id);

            return this.View(cars);
        }

        public HttpResponse Add()
        {
            if (this.userService.IsMechanic(this.User.Id))
            {
                return this.Unauthorized();
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCarViewModel input)
        {
            var errorsMessage = this.validator.CarValidator(input);

            if (errorsMessage.Any())
            {
                return this.Error(errorsMessage);
            }

            input.OwnerId = this.User.Id;

            this.carService.AddCar(input);

            return this.Redirect("/Cars/All");
        }
    }
}
