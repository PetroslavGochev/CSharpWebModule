namespace IRunes.Controllers
{
    using IRunes.Services;
    using IRunes.ViewModel.Track;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    [Authorize]
    public class TracksController : Controller
    {
        private readonly IValidatorService validatorService;
        private readonly ITrackService trackService;

        public TracksController(IValidatorService validatorService, ITrackService trackService)
        {
            this.validatorService = validatorService;
            this.trackService = trackService;
        }

        public HttpResponse Create(string albumId)
        {
            var model = new CreateTrackViewModel()
            {
                AlbumId = albumId
            };

            return this.View(model);
        }

        [HttpPost]
        public HttpResponse Create(CreateTrackViewModel model)
        {
                var errors = this.validatorService.TrackValidator(model);

            if (errors.Any())
            {
                return this.View(model);
            }

            this.trackService.CreateTrack(model);

            return this.Redirect($"/Albums/Details?id={model.AlbumId}");
        }

        public HttpResponse Details(string albumId, string trackId)
        {
            var model = this.trackService.Details(trackId);

            model.AlbumId = albumId;

            return this.View(model);
        }
    }
}
