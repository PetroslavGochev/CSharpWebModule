using SIS.HTTP.Models;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using System.IO;

namespace SulsApp.Controllers
{
    public class HomeController : Controller
    {
        public  HttpResponse Index(HttpRequest request)
        {
            return this.View();
        }
    }
}
