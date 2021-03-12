using SIS.HTTP.Enumerators;
using SIS.HTTP.Models;
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
            string content = "<h1>Welcome to contact page!</h1>";
            byte[] stringContent = Encoding.UTF8.GetBytes(content);
            var response = new HttpResponse(HttpResponseCode.OK, stringContent);
            response.Headers.Add(new Header("Content-Type", "text/html"));
            return response;
        }

        private static HttpResponse Index(HttpRequest httpRequest)
        {
            string content = "<h1>Welcome to home page!</h1><img src='https://www.findupet.com/uploads/petgalleryfile/images/940x640/IMG_20190517_124637.jpg'/>";
            byte[] stringContent = Encoding.UTF8.GetBytes(content);
            var response = new HttpResponse(HttpResponseCode.OK, stringContent);
            response.Headers.Add(new Header("Content-Type", "text/html"));
            return response;
        }
        private static HttpResponse LoginPage(HttpRequest httpRequest)
        {
            string content = "<h1>Welcome to login page!</h1>";
            byte[] stringContent = Encoding.UTF8.GetBytes(content);
            var response = new HttpResponse(HttpResponseCode.OK, stringContent);
            response.Headers.Add(new Header("Content-Type", "text/html"));
            return response;
        }
        private static HttpResponse DoLogin(HttpRequest httpRequest)
        {
            string content = "<h1>You succsesfully login!</h1>";
            byte[] stringContent = Encoding.UTF8.GetBytes(content);
            var response = new HttpResponse(HttpResponseCode.OK, stringContent);
            response.Headers.Add(new Header("Content-Type", "text/html"));
            return response;
        }
    }   
}
