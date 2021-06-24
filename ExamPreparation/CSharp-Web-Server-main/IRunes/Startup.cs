namespace IRunes
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using IRunes.Data;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using IRunes.Services;

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
                       .Add<IAlbumService, AlbumService>()
                       .Add<ITrackService, TrackService>()
                       .Add<IUserService, UserService>()
                       .Add<ApplicationDbContext>())
                   .WithConfiguration<ApplicationDbContext>(context => context
                       .Database.Migrate())
                   .Start();
        }
    
}
