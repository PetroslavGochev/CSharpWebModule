namespace Skeleton
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using Skeleton.Data;
    using Skeleton.Services;
    using Skeleton.Services.Contracts;

    public class Startup
    {
        public static async Task Main()
         => await HttpServer
             .WithRoutes(routes => routes
                 .MapStaticFiles()
                 .MapControllers())
             .WithServices(services => services
                 .Add<IViewEngine, CompilationViewEngine>()
                 .Add<IValidatorService, ValidatorService>()
                 .Add<IPasswordService, PasswordService>()
                 .Add<IUserService, UserService>()
                 .Add<ApplicationDbContext>())
             .WithConfiguration<ApplicationDbContext>(context => context
                 .Database.Migrate())
             .Start();
    }
}
