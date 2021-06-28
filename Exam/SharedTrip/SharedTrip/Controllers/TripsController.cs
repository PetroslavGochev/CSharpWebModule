namespace SharedTrip.Controllers
{
    using System.Linq;

    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Services.Contracts;
    using SharedTrip.ViewModel.Trip;

    using static Common.PathConstants;

    [Authorize]
    public class TripsController : Controller
    {
        private readonly IValidatorService validatorService;
        private readonly ITripService tripService;

        public TripsController(IValidatorService validatorService, ITripService tripService)
        {
            this.validatorService = validatorService;
            this.tripService = tripService;
        }

        public HttpResponse All()
        {
            var allTrip = this.tripService.AllTrip();
            return this.View(allTrip);
        }

        public HttpResponse Add()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddTripViewModel model)
        {
            var errors = this.validatorService.TripsValidation(model);

            if (errors.Any())
            {
                return this.View();
            }

            this.tripService.CreateTrip(model);
            
            return this.Redirect(TripsAll);
        }

        public HttpResponse Details(string tripId)
        {
            var model = this.tripService.Details(tripId);

            return this.View(model);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (this.tripService.IsTripUserExist(this.User.Id, tripId))
            {
                return Redirect(string.Format(TripsDetails, tripId));
            }

            this.tripService.AddUserToTrip(tripId, this.User.Id);

            return this.Redirect(TripsAll);

        }
    }
}
