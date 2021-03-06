namespace BattleCards
{
    using System;
    using System.Threading.Tasks;

    using BattleCards.Data;
    using BattleCards.Services;
    using Microsoft.EntityFrameworkCore;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;

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
                   .Add<ICardService, CardService>()
                   .Add<ApplicationDbContext>())
               .WithConfiguration<ApplicationDbContext>(context => context
                   .Database.Migrate())
               .Start();
    }
}
