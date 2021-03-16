using SIS.HTTP.Models;
using SIS.HTTP.Response;
using System.IO;

namespace SulsApp.Controllers
{
    public class UsersController
    {
        public HttpResponse Login(HttpRequest request)
        {
            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var html = File.ReadAllText("Views/Login/Login.html");
            var bodyWithLayout = layout.Replace("@RenderBody()", html);
            return new HtmlResponse(bodyWithLayout);
        }

        public HttpResponse Register(HttpRequest request)
        {
            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var html = File.ReadAllText("Views/Login/Register.html");
            var bodyWithLayout = layout.Replace("@RenderBody()", html);
            return new HtmlResponse(bodyWithLayout);
        }
    }
}
