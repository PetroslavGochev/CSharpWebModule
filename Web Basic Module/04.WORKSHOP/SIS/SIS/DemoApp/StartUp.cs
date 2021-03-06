using DemoApp.Data.Model;
using SIS.HTTP.Enumerators;
using SIS.HTTP.Models;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DemoApp
{
    public class Startup : IMvcApplication
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public void Configure(IList<Route> routeTable)
        {
            routeTable.Add(new Route("/", Index, HttpMethodType.Get));
            routeTable.Add(new Route("/Tweets/Create", CreateTweets, HttpMethodType.Post));
            routeTable.Add(new Route("/favicon.ico", FavIcon, HttpMethodType.Get));
        }

        public void ConfigureServices()
        {    
            db.Database.EnsureCreated();
        }
        private static HttpResponse CreateTweets(HttpRequest httpRequest)
        {
            db.Tweets.Add(new Tweet()
            {
                CreatedOn = DateTime.UtcNow,
                Creator = httpRequest.FormData["creator"],
                Content = httpRequest.FormData["tweetName"]
            });
            db.SaveChanges();

            return new RedirectResponse("/");
        }

        private static HttpResponse FavIcon(HttpRequest arg)
        {
            var byteContext = File.ReadAllBytes("wwwroot/favicon.ico");
            return new FileResponse(byteContext, "image/x-icon");
        }

        private static HttpResponse Index(HttpRequest httpRequest)
        {
            var username = httpRequest.SessionData.ContainsKey("Username") ?
                httpRequest.SessionData["Username"] : "Anonymous";

            var tweets = db.Tweets.Select(t => new
            {
                t.CreatedOn.Date,
                t.Creator,
                t.Content
            }).ToArray();

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<table><tr><th>Date</th><th>Creator</th><th>Content</th></tr>");
            foreach (var tweet in tweets)
            {
                htmlBuilder.Append($"<tr><td>{tweet.Date}</td><td>{tweet.Creator}</td><td>{tweet.Content}</td></tr>");
            }
            htmlBuilder.Append("</table>");
            htmlBuilder.Append($"<form action = '/Tweets/Create' method= 'post'><input name='creator'/><br/><textarea name = 'tweetName'></textarea><br/><input type = 'submit' /></form>");

            return new HtmlResponse(htmlBuilder.ToString());
        }
    }
}
}
