namespace MyWebServer.Controllers
{
    using System;

    using MyWebServer.Service.Controllers;
    using MyWebServer.Service.Http;

    public class HomeController : Controller
    {
        public HomeController(HttpRequest request)
            : base(request)
        {
        }

        public HttpResponse Index()
            => this.Text("Hello");

        public HttpResponse LocalRedirect()
           => this.Redirect("/Cats");

        public HttpResponse ToSoftUni()
            => this.Redirect("https://softuni.bg");

        public HttpResponse Error() => throw new InvalidOperationException("Invalid action!");
    }
}
