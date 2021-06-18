namespace MyWebServer.Service.Controllers
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Identity;
    using MyWebServer.Service.Result;
    using MyWebServer.Service.Results;
    using System.Runtime.CompilerServices;

    public abstract class Controller
    {
        private UserIdentity userIdentity;

        protected Controller(HttpRequest request)
        {
            this.Request = request;
        }
        protected HttpRequest Request { get; private set; }

        protected HttpResponse Response { get; private set; }

        protected UserIdentity User
        {
            get
            {
                if (this.userIdentity == null)
                {
                    this.userIdentity = this.Request.Session.Contains(UserSessionKey)
                        ? new UserIdentity { Id = this.Request.Session[UserSessionKey] }
                        : new();
                }

                return this.userIdentity;
            }
        }

        protected ActionResult Text(string text)
            => new TextResult(this.Response, text);

        protected ActionResult Html(string html)
            => new HtmlResult(this.Response, html);

        protected ActionResult Redirect(string location)
            => new RedirectResult(this.Response, location);

        protected ActionResult View([CallerMemberName] string viewName = "")
            => new ViewResult(this.Response, viewName, this.GetControllerName(), null);

        protected ActionResult View(object model, [CallerMemberName] string viewName = "")
            => new ViewResult(this.Response, viewName, this.GetControllerName(), model);

        protected ActionResult View(string viewName, object model)
             => new ViewResult(this.Response, viewName, this.GetControllerName(), model);

        private string GetControllerName()
            => this.GetType().Name.Replace(nameof(Controller), string.Empty);
    }
}