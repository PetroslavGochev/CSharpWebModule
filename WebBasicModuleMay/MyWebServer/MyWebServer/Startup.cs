namespace MyWebServer
{
    using MyWebServer.Service;
    using System.Threading.Tasks;

    public class Startup
    {
        public static async Task Main(string[] args)
        {
            var server = new HttpServer("127.0.0.1", 9090);

            await server.Start();
        }
    }
}
