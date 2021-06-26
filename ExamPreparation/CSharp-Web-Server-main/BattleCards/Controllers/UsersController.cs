namespace BattleCards.Controllers
{
    using System.Linq;

    using BattleCards.Services;
    using BattleCards.ViewModels.Users;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using static Common.PathConstants;

    public class UsersController : Controller
    {
        private static string InvalidUserOrPassword = "Invalid username or password. Please try again!";

        private readonly IUserService userService;
        private readonly IValidatorService validatorService;

        public UsersController(
            IUserService userService,
            IValidatorService validatorService)
        {
            this.userService = userService;
            this.validatorService = validatorService;
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
                return this.Error(InvalidUserOrPassword);
            }

            this.SignIn(userId);
            return this.Redirect(AllCards);
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
                return this.Error(errors);
            }

            this.userService.Register(model);

            return this.Redirect(UserLogin);
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect(DefaultRouthe);
        }
    }
}
