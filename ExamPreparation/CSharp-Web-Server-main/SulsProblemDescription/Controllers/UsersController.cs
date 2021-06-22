namespace SulsProblemDescription.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SulsProblemDescription.Models.User;
    using SulsProblemDescription.Services;
    using System.Linq;

    public class UsersController : Controller
    {
        private readonly IUserService userSerivce;
        private readonly IValidatorService validatorService;

        public UsersController(
            IUserService userSerivce,
            IValidatorService validatorService)
        {
            this.userSerivce = userSerivce;
            this.validatorService = validatorService;
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterViewModel model)
        {
            var errors = this.validatorService.UserValidation(model);

            if (errors.Any())
            {
                return this.Error(errors);
            }

            this.userSerivce.Register(model);

            return this.View("/Users/Login");
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            var userId = this.userSerivce.Login(username, password);

            if (userId == string.Empty)
            {
                return this.Error("InvalidPassword");
            }

            this.SignIn(userId);

            return this.Redirect("/Home/IndexLoggedIn");
        }

        public HttpResponse LogOut()
        {
            this.SignOut();

            return this.View("/Home/Index");
        }
    }
}
