using SIS.HTTP.Models;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using System.IO;

namespace SulsApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse Register(HttpRequest request)
        {
            return this.View();
        }
    }
}
