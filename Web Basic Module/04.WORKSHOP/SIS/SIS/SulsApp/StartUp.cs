using SIS.HTTP.Models;
using SIS.MvcFramework;
using SulsApp.Services;
using System.Collections.Generic;

namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserService, UserService>();
        }
        public void Configure(IList<Route> routeTable)
        {
            var db = new SulsAppDbContext();
            db.Database.EnsureCreated();
        }
    }
}
