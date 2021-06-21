namespace CarShop.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class HomeController : Controller
    {

        public HttpResponse Index()
        {
            if (this.User.Id != null)
            {
                return this.Unauthorized();
            }
            return this.View();
        }
    }
}
