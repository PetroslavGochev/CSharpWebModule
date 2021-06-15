namespace MyWebServer.Controllers
{
    using MyWebServer.Service.Controllers;
    using MyWebServer.Service.Http;

    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request)
            : base(request)
        {
        }

        public HttpResponse Cats()
        {
            const string nameKey = "Name";

            var query = this.Request.Query;

            var catName = query.ContainsKey(nameKey)
                ? query[nameKey]
                : "the cats";

            var result = $"<h1>Hello from {catName} </h1>";

            return this.Html(result);
        }

        public HttpResponse Dogs()
            => this.Html("<h1>Hello dog<h1>");
    }
}
