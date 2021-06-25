namespace Panda.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using Panda.Services;

    [Authorize]
    public class ReceiptsController : Controller
    {
        private readonly IRecipientService recipientService;

        public ReceiptsController(IRecipientService recipientService)
        {
            this.recipientService = recipientService;
        }

        public HttpResponse Index()
        {
            var model = this.recipientService.All(this.User.Id);
            return this.View(model);
        }
    }
}
