using SIS.HTTP.Models;
using System.Collections.Generic;

namespace SIS.MvcFramework
{
    public interface IMvcApplication
    {
        void ConfigureServices(IServiceCollection serviceCollection);
        void Configure(IList<Route> routeTable);

    }
}
