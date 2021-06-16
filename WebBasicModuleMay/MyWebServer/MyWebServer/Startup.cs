namespace MyWebServer
{
    using System.Threading.Tasks;

    using MyWebServer.Service;
    using MyWebServer.Service.Controllers;
    using MyWebServer.Controllers;

    public class Startup
    {
        public static async Task Main()
                => await new HttpServer(routes => routes
                    .MapGet<HomeController>("/", c => c.Index())
                    .MapGet<HomeController>("/Softuni", c => c.ToSoftUni())
                    .MapGet<HomeController>("/ToCats", c => c.LocalRedirect())
                    .MapGet<AnimalsController>("/Cats", c => c.Cats())
                    .MapGet<AnimalsController>("/Bunnies", c => c.Bunnies())
                    .MapGet<AnimalsController>("/Turtles", c => c.Turtles())
                    .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
                    .MapGet<CatsController>("/Cats/Create", c => c.Create())
                    .MapGet<CatsController>("/Cats/Save", c => c.Save()))
                   .Start();
    }
}
