namespace MyWebServer
{
    using MyWebServer.Service;
    using MyWebServer.Service.Results;
    using System;
    using System.Threading.Tasks;

    public class Startup
    {
        public static async Task Main(string[] args)
                => await new HttpServer(routes => routes
                    .MapGet("/", new TextResponse("Hello"))
                    .MapGet("/Cats", new TextResponse("Hello cat"))
                    .MapGet("/Dogs", new TextResponse("Hello dog")))
                  .Start();
    }
}
