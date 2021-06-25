using MyWebServer.Controllers;
using MyWebServer.Http;
using Panda.Services;
using Panda.ViewModels.User;
using System.Linq;

namespace Panda.Controllers
{
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
                return this.View();
            }

            this.SignIn(userId);

            return this.Redirect("/Home/Index");
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
                return this.View();
            }

            this.userService.Register(model);

            return this.Redirect("/Users/Login");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/Home/Index");
        }
    }
}
