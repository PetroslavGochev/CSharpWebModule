namespace MyWebServer.Service.Controllers
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Response;
    using MyWebServer.Service.Results;

    public abstract class Controller
    {
        protected Controller(HttpRequest request)
        {
            this.Request = request;
        }
        protected HttpRequest Request { get; private set; }

        protected HttpResponse Text(string text)
            => new TextResponse(text);

        protected HttpResponse Html(string html)
            => new HtmlResponse(html);

        protected HttpResponse Redirect(string location)
            => new RedirectResponse(location);
    }
}