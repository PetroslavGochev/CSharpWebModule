namespace CarShop.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using CarShop.Models.Users;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class UsersController : Controller
    {
        private const string InvalidUsername = "Invalid username or password, please try again";

        private readonly IValidator validator;
        private readonly IUserService userService;

        public UsersController(
            IValidator validator,
            IUserService userService)
        {
            this.validator = validator;
            this.userService = userService;
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserViewModel input)
        {
            var user = this.userService.UserLogin(input);
            if (user == null)
            {
                return this.Error(InvalidUsername);
            }

            this.SignIn(user.Id);
            return this.Redirect("/Cars/All");
        }
        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserViewModel model)
        {
            var errors = this.validator.UserValidator(model);

            if (errors.Any())
            {
                return this.Error(errors);
            }

            this.userService.Register(model);
            return this.Redirect("/Users/Login");
        }

    }
}
