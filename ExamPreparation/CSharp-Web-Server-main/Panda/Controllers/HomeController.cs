namespace Panda.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using Panda.Services;
    using Panda.ViewModels.User;

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
                return this.Redirect("/Home/IndexLoggedIn");
            }

            return this.View();
        }

        public HttpResponse IndexLoggedIn()
        {
            var model = new LoginViewModel()
            {
                Username = this.userService.CurrentUsername(this.User.Id)
            };

            return this.View(model);
        }
    }
}
