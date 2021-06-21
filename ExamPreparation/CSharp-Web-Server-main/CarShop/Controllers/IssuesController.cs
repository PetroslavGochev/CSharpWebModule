namespace CarShop.Controllers
{
    using System.Linq;
    using CarShop.Models.Issues;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    [Authorize]
    public class IssuesController : Controller
    {
        private readonly ICarService carService;
        private readonly IIssueService issueService;
        private readonly IValidator validator;
        private readonly IUserService userService;

        public IssuesController(
            ICarService carService,
            IIssueService issueService,
            IValidator validator,
            IUserService userService)
        {
            this.carService = carService;
            this.issueService = issueService;
            this.validator = validator;
            this.userService = userService;
        }

        public HttpResponse CarIssues(string carId)
        {
            var model = this.carService.CarIssues(carId);

            return this.View(model);
        }

        public HttpResponse Add(string carId)
        {
            var addIssues = new AddIssuesViewModel()
            {
                CarId = carId
            };
            return this.View(addIssues);
        }

        [HttpPost]
        public HttpResponse Add(string carId, string description)
        {
            var error = this.validator.IssueValidator(description);

            if (error.Any())
            {
                return this.Error(error);
            }

            this.issueService.Add(carId, description);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }

        public HttpResponse Delete(string issueId, string carId)
        {
            this.issueService.Delete(issueId);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }

        public HttpResponse Fix(string issueId, string carId)
        {
            if (!this.userService.IsMechanic(this.User.Id))
            {
                return this.Unauthorized();
            }

            this.issueService.Fix(issueId);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }
    }
}
