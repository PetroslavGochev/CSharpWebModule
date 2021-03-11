using SIS.HTTP.Server;
using System;
using System.Threading.Tasks;

namespace DemoApp
{
    class StartUp
    {
        static async Task Main(string[] args)
        {
            //Actions:
            // => response IndexPage()
            // /favicon.ico => favicon.ico
            // GET/Contact => response ShowContactFrom()
            // POST/Contact => response FillConctactFrom()

            var httpServer = new HttpServer(80);
            await httpServer.StartAsync();
        }
    }
}
