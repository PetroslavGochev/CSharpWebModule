namespace SharedTrip.Controllers
{
    using System.Linq;

    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Services.Contracts;
    using SharedTrip.ViewModel.User;

    using static Common.PathConstants;

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
            var errors = this.validatorService.UserValidation(model);

            if (errors.Any())
            {
                return this.View();
            }

            this.userService.RegisterUser(model);

            return this.Redirect(UsersLogin);
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
            return this.Redirect(TripsAll);
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect(DefaultRouthe);
        }
    }
}
