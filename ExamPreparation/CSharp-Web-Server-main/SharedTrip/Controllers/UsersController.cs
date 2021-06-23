namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Services;
    using SharedTrip.ViewModel.User;
    using System.Linq;

    public class UsersController : Controller
    {
        private readonly IValidatorService validatorService;
        private readonly IUserService userService;

        public UsersController(IValidatorService validatorService, IUserService userService)
        {
            this.validatorService = validatorService;
            this.userService = userService;
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterViewModel model)
        {
            var errors = this.validatorService.UserValidator(model);

            if (errors.Any())
            {
                this.Error(errors);
            }

            this.userService.RegisterUser(model);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            var userId = this.userService.Login(username, password);

            if (userId == string.Empty)
            {
                return this.View();
            }

            this.SignIn(userId);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/Home/Index");
        }
    }
}
