namespace SulsProblemDescription
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using SulsApp.Data;
    using SulsProblemDescription.Services;

    public class Startup
    {
        static async Task Main(string[] args)      
           => await HttpServer
               .WithRoutes(routes => routes
                   .MapStaticFiles()
                   .MapControllers())
               .WithServices(services => services
                   .Add<IViewEngine, CompilationViewEngine>()
                   .Add<IPasswordService, PasswordService>()
                   .Add<IUserService, UserService>()
                   .Add<IProblemService, ProblemService>()
                   .Add<ISubmissionService, SubmissionService>()
                   .Add<IValidatorService, ValidatorService>()
                   .Add<ApplicationDbContext>())
               .WithConfiguration<ApplicationDbContext>(context => context
                   .Database.Migrate())
               .Start();
        
    }
}
