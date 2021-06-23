namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Services;
    using SharedTrip.ViewModel.Trip;
    using System.Linq;

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
            var errors = this.validatorService.TripsValidator(model);

            if (errors.Any())
            {
                return this.Error(errors);
            }

            this.tripService.CreateTrip(model);
            
            return this.Redirect("/Trips/All");
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
                return Redirect($"/Trips/Details?tripId={tripId}");
            }

            this.tripService.AddUserToTrip(tripId, this.User.Id);

            return this.Redirect("/Trips/All");

        }
    }
}
