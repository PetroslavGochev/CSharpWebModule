namespace Skeleton.Controllers
{
    using System.Linq;

    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using Skeleton.Services.Contracts;
    using Skeleton.ViewModels.User;

    using static Common.GlobalConstants;

    public class UsersController : Controller
    {
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
            return this.Redirect("/");
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

            return this.Redirect("/");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }
    }
}
