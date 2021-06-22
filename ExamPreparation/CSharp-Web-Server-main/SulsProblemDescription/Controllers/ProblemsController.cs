namespace SulsProblemDescription.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SulsProblemDescription.Models.Problem;
    using SulsProblemDescription.Services;
    using System.Linq;


    [Authorize]
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;
        private readonly IValidatorService validatorService;

        public ProblemsController(IProblemService problemService, IValidatorService validatorService)
        {
            this.problemService = problemService;
            this.validatorService = validatorService;
        }

        public HttpResponse Create()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateProblemViewModel model)
        {
            var errors = this.validatorService.ProblemValidation(model);

            if (errors.Any())
            {
                return this.Error(errors);
            }

            this.problemService.CreateProblem(model);
            return this.Redirect("/Home/IndexLoggedIn");
        }

        public HttpResponse Details(string id)
        {
            var model = this.problemService.DetailsProblem(id);

            return this.View(model);
        }
    }
}
