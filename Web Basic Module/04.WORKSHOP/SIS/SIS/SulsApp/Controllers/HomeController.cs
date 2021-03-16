using SIS.HTTP.Models;
using SIS.HTTP.Response;
using System.IO;

namespace SulsApp.Controllers
{
    public class HomeController
    {
        public  HttpResponse Index(HttpRequest request)
        {
            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var html = File.ReadAllText("Views/Home/Index.html");
            var bodyWithLayout = layout.Replace("@RenderBody()", html);
            return new HtmlResponse(bodyWithLayout);
        }
    }
}
