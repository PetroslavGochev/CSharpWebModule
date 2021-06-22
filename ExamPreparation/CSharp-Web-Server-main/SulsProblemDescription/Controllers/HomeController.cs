namespace SulsProblemDescription.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SulsProblemDescription.Services;

    public class HomeController : Controller
    {
        private readonly IProblemService problemService;

        public HomeController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        public HttpResponse Index()
        {
            if (this.User.Id != null)
            {
                return this.Unauthorized();
            }
            return this.View();
        }

        [Authorize]
        public HttpResponse IndexLoggedIn()
        {
            var model = this.problemService.AllProblems();

            return this.View(model);
        }
    }
}
