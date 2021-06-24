namespace IRunes.Controllers
{
    using IRunes.Services;
    using IRunes.ViewModel.User;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class HomeController : Controller
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        public HttpResponse Index()
        {
            if (this.User.Id != null)
            {
                var model = new LoginViewModel()
                {
                    Username = this.userService.CurrentUserName(this.User.Id)
                };

                return this.View(model, "Home");
            }
            return this.View();
        }
    }
}
