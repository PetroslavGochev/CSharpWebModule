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
            routeTable.Add(new Route(HttpMethodType.Get,"/", new HomeController().Index));
            routeTable.Add(new Route(HttpMethodType.Get, "/Users/Login", new UsersController().Login));
            routeTable.Add(new Route(HttpMethodType.Post, "/Users/Login", new UsersController().DoLogin));
            routeTable.Add(new Route(HttpMethodType.Get, "/Users/Register", new UsersController().Register));
            routeTable.Add(new Route(HttpMethodType.Post, "/Users/Register", new UsersController().DoRegister));
        }

        public void ConfigureServices()
        {
            var db = new SulsAppDbContext();
            db.Database.EnsureCreated();
        }
    }
}
