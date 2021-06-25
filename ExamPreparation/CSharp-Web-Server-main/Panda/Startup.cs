namespace Panda
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using Panda.Data;
    using Panda.Services;

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
                       .Add<IPackageService, PackageService>()
                       .Add<IRecipientService, RecipientService>()
                       .Add<IValidatorService, ValidatorService>()
                       .Add<ApplicationDbContext>())
                   .WithConfiguration<ApplicationDbContext>(context => context
                       .Database.Migrate())
                   .Start();
    }
}
