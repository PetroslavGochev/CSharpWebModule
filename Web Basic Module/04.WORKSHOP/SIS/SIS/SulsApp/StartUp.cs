using SIS.HTTP.Models;
using SIS.MvcFramework;
using System.Collections.Generic;

namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            var db = new SulsAppDbContext();
            db.Database.EnsureCreated();
        }

        public void ConfigureServices()
        {
            
        }
    }
}
