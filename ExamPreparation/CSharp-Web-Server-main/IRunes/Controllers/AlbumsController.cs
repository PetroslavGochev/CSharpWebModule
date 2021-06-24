namespace IRunes.Controllers
{
    using IRunes.Services;
    using IRunes.ViewModel.Album;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    [Authorize]
    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IValidatorService validatorService;

        public AlbumsController(IAlbumService albumService, IValidatorService validatorService)
        {
            this.albumService = albumService;
            this.validatorService = validatorService;
        }

        public HttpResponse All()
        {
            var albums = this.albumService.All();

            return this.View(albums);
        }

        public HttpResponse Create()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateAlbumViewModel model)
        {
            var errors = this.validatorService.AlbumValidator(model);

            if (errors.Any())
            {
                return this.View();
            }

            this.albumService.CreateAlbum(model);

            return this.Redirect("/Albums/All");
        }

        public HttpResponse Details(string id)
        {
            var album = this.albumService.Detail(id);

            return this.View(album);
        }
    }
}
