namespace SulsProblemDescription.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SulsProblemDescription.Models.Problem;
    using SulsProblemDescription.Models.Submission;
    using SulsProblemDescription.Services;
    using System.Linq;


    [Authorize]
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionService submissionService;
        private readonly IValidatorService validatorService;

        public SubmissionsController(
            ISubmissionService submissionService,
            IValidatorService validatorService)
        {
            this.submissionService = submissionService;
            this.validatorService = validatorService;
        }

        public HttpResponse Create(SubmitSubmissionViewModel model)
        {
            return this.View(model);
        }

        [HttpPost]
        public HttpResponse Create(CreateSubmissionViewModel model)
        {
            var errors = this.validatorService.SubmissionValidation(model.Code);

            if (errors.Any())
            {
                return this.Error(errors);
            }

            model.UserId = this.User.Id;

            this.submissionService.CreateSubmission(model);

            return this.Redirect("/Home/IndexLoggedIn");
        }

        public HttpResponse Delete(string id)
        {
            this.submissionService.Delete(id);

            return this.Redirect("/Home/IndexLoggedIn");
        }
    }
}
