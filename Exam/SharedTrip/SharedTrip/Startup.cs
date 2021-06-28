namespace SharedTrip
{
    using System.Threading.Tasks;

    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using SharedTrip.Services;
    using SharedTrip.Data;
    using Microsoft.EntityFrameworkCore;
    using SharedTrip.Services.Contracts;

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
                   .Add<IValidatorService, ValidatorService>()
                   .Add<IUserService, UserService>()
                   .Add<ITripService, TripService>()
                   .Add<ApplicationDbContext>())
               .WithConfiguration<ApplicationDbContext>(context => context
                   .Database.Migrate())
               .Start();
    }
}
