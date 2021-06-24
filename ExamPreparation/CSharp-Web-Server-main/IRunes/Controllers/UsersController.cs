namespace IRunes.Controllers
{
    using System.Linq;

    using IRunes.Services;
    using IRunes.ViewModel.User;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IValidatorService validatorService;

        public UsersController(IUserService userService, IValidatorService validatorService)
        {
            this.userService = userService;
            this.validatorService = validatorService;
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
                return this.Error(errors);
            }

            this.userService.Register(model);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginViewModel model)
        {
            var userId = this.userService.Login(model);

            if (userId == string.Empty)
            {
                return this.View();
            }

            this.SignIn(userId);

            return this.Redirect("/Home/Index");
        }
        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/Home/Index");
        }
    }
}
