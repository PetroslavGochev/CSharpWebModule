namespace MyWebServer.Service.Controllers
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Response;
    using MyWebServer.Service.Results;
    using System.Runtime.CompilerServices;

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

        protected HttpResponse View([CallerMemberName] string viewName = "")
            => new ViewResponse(viewName, this.GetControllerName(), null);

        protected HttpResponse View(object model, [CallerMemberName] string viewName = "")
            => new ViewResponse(viewName, this.GetControllerName(), model);

        protected HttpResponse View(string viewName, object model)
             => new ViewResponse(viewName, this.GetControllerName(), model);

        private string GetControllerName()
            => this.GetType().Name.Replace(nameof(Controller), string.Empty);
    }
}