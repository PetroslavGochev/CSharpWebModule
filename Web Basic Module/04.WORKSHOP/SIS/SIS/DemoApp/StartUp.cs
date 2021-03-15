using SIS.HTTP.Enumerators;
using SIS.HTTP.Models;
using SIS.HTTP.Response;
using SIS.HTTP.Server;
using System;
using System.Collections.Generic;
using System.IO;
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
            routeTable.Add(new Route("/Tweets/Create", CreateTweets, HttpMethodType.Post));
            routeTable.Add(new Route("/favicon.ico", FavIcon, HttpMethodType.Get));  

            var httpServer = new HttpServer(80,routeTable);
            await httpServer.StartAsync();
        }

        private static HttpResponse CreateTweets(HttpRequest httpRequest)
        {
            return new HtmlResponse("");
        }

        private static HttpResponse FavIcon(HttpRequest arg)
        {
            var byteContext = File.ReadAllBytes("wwwroot/favicon.ico");
            return new FileResponse(byteContext,"image/x-icon");
        }

        private static HttpResponse Index(HttpRequest httpRequest)
        {
            var username = httpRequest.SessionData.ContainsKey("Username") ?
                httpRequest.SessionData["Username"] : "Anonymous";
            return new HtmlResponse($"<form action = '/Tweets/Create' method= 'post'><textarea name = 'tweetName'></textarea><input type = 'submit' /></form>");
        }
    }   
}
