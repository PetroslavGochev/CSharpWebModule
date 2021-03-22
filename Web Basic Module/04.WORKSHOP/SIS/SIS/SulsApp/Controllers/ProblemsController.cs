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

        [HttpPost("/Problems/Create")]
        public HttpResponse DoCreate(string name,string points)
        {
            if(!int.TryParse(points, out int intValue))
            {
                return this.Error("Invalid points");
            }
            this.problemService.CreateProblem(name, intValue);
            return this.Redirect("/");
        }
    }
}
