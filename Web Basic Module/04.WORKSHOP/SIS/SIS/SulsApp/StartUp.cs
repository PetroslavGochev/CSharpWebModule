using SIS.HTTP.Enumerators;
using SIS.HTTP.Models;
using SIS.MvcFramework;
using SulsApp.Controllers;
using System.Collections.Generic;

namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            routeTable.Add(new Route("/", new HomeController().Index, HttpMethodType.Get));
            routeTable.Add(new Route("/css/bootstrap.min.css", new StaticFilesController().Bootstrap, HttpMethodType.Get));
            routeTable.Add(new Route("/css/site.css", new StaticFilesController().Site, HttpMethodType.Get));
            routeTable.Add(new Route("/css/reset.css", new StaticFilesController().Reset, HttpMethodType.Get));
            routeTable.Add(new Route("/Users/Login", new UsersController().Login, HttpMethodType.Get));
            routeTable.Add(new Route("/Users/Register", new UsersController().Register, HttpMethodType.Get));
        }

        public void ConfigureServices()
        {
            var db = new SulsAppDbContext();
            db.Database.EnsureCreated();
        }
    }
}
