namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using static Common.PathConstants;

    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
            if (this.User.Id != null)
            {
                return this.Redirect(TripsAll);
            }
            return this.View();
        }
    }
}
