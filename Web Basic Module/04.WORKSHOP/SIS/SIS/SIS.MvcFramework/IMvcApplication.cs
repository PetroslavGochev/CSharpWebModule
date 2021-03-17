using SIS.HTTP.Models;
using System.Collections.Generic;

namespace SIS.MvcFramework
{
    public interface IMvcApplication
    {
        void Configure(IList<Route> routeTable);
        void ConfigureServices();
        
    }
}
