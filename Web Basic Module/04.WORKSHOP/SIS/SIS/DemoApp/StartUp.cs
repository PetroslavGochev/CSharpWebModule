using SIS.HTTP.Enumerators;
using SIS.HTTP.Models;
using SIS.HTTP.Response;
using SIS.HTTP.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    class StartUp
    {
        static async Task Main(string[] args)
        {
            var routeTable = new List<Route>();
            routeTable.Add(new Route("/", Index, HttpMethodType.Get));
            routeTable.Add(new Route("/users/login", LoginPage, HttpMethodType.Get));
            routeTable.Add(new Route("/users/login", DoLogin, HttpMethodType.Post));
            routeTable.Add(new Route("/contact", Contact, HttpMethodType.Get)); 
            routeTable.Add(new Route("/favicon.ico", FavIcon, HttpMethodType.Get)); 
            //Actions:
            // => response IndexPage()
            // /favicon.ico => favicon.ico
            // GET/Contact => response ShowContactFrom()
            // POST/Contact => response FillConctactFrom()

            var httpServer = new HttpServer(80,routeTable);
            await httpServer.StartAsync();
        }

        private static HttpResponse FavIcon(HttpRequest arg)
        {
            throw new NotImplementedException();
        }

        private static HttpResponse Contact(HttpRequest httpRequest)
        {
           return new HtmlResponse("<h1>Welcome to contact page!</h1>");      
        }

        private static HttpResponse Index(HttpRequest httpRequest)
        {
            return new HtmlResponse("<h1>Welcome to home page!</h1><img src='https://www.findupet.com/uploads/petgalleryfile/images/940x640/IMG_20190517_124637.jpg'/>");
        }
        private static HttpResponse LoginPage(HttpRequest httpRequest)
        {
            return new HtmlResponse("<h1>Welcome to login page!</h1>");
        }
        private static HttpResponse DoLogin(HttpRequest httpRequest)
        {
            return new HtmlResponse("<h1>You succsesfully login!</h1>");
        }
    }   
}
