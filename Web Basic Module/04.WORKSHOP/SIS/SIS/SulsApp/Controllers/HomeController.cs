using SIS.HTTP.Models;
using SIS.HTTP.Response;

namespace SulsApp.Controllers
{
    public class HomeController
    {
        public  HttpResponse Index(HttpRequest request)
        {
            return new HtmlResponse("<h1>Hello, Roska</h1>");
        }
    }
}
