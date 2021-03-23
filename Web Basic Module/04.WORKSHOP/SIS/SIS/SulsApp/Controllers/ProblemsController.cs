using SIS.HTTP.Models;
using SIS.MvcFramework;
using SulsApp.Services;

namespace SulsApp.Controllers
{
    public class ProblemsController : Controller
    {
        private IProblemService problemService;
        public ProblemsController(IProblemService problemService)
        {
            this.problemService = problemService; 
        }

        public HttpResponse Create()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name,int points)
        {
            if (string.IsNullOrEmpty(name))
            {
                return this.Error("Invalid name");
            }

            if(points <= 0 || points > 100)
            {
                return this.Error("Poits range: [1-100]");
            }
            this.problemService.CreateProblem(name, points);
            return this.Redirect("/");
        }
    }
}
