//using SIS.HTTP.Models;
//using SIS.HTTP.Response;
//using System.IO;
//using System.Runtime.CompilerServices;

//namespace SIS.MvcFramework
//{
//    public abstract class Controller
//    {
//        private const string RENDER_BODY = "@RenderBody()";
//        private const string VIEWS = "Views/";
//        private const string LAYOUT = "Shared/_Layout";
//        private const string HTML = ".html";
//        private const string CONTROLLER = "Controller";

//      protected HttpResponse View<T>(T viewModel = null, [CallerMemberName] string viewName = null)
//            where T : class
//        {
//            IViewEngine viewEngine = new ViewEngine();
//            var typeName = this.GetType().Name/*.Replace(CONTROLLER, string.Empty)*/;
//            var controllerName = typeName.Substring(0, typeName.Length - 10);
//            var html = File.ReadAllText(VIEWS + controllerName + "/" + viewName + HTML);
//            html = viewEngine.GetHtml(html, null);

//            var layout = File.ReadAllText(VIEWS + LAYOUT + HTML);
//            var bodyWithLayout = layout.Replace(RENDER_BODY, html);
//            return new HtmlResponse(bodyWithLayout);
//        }
//        protected HttpResponse View([CallerMemberName]string viewName = null)
//        {
//            return this.View<object>(null, viewName);
//        }
//    }
//}
using System;
using SIS.HTTP;
using SIS.HTTP.Response;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using SIS.HTTP.Models;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {

        public HttpRequest Request { get; set; }

        protected HttpResponse View<T>(T viewModel = null, [CallerMemberName] string viewName = null)
        where T : class
        {
            var typeName = this.GetType().Name;
            var controllerName = typeName.Substring(0, typeName.Length - 10);
            var viewPath = "Views/" + controllerName + "/" + viewName + ".html";
            return this.ViewByName<T>(viewPath, viewModel);
        }
        protected HttpResponse View([CallerMemberName] string viewName = null)
        {
            return this.View<object>(null, viewName);

        }

        protected HttpResponse Error(string error)
        {
            return this.ViewByName<ErrorViewModel>("Views/Shared/Error.html", new ErrorViewModel { Error = error });
        }

        protected HttpResponse Redirect(string url)
        {
            return new RedirectResponse(url);
        }

        private HttpResponse ViewByName<T>(string viewPath, object viewModel)
        {
            IViewEngine viewEngine = new ViewEngine();

            var html = File.ReadAllText(viewPath);
            html = viewEngine.GetHtml(html, viewModel,this.User);

            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var bodyWithLayout = layout.Replace("@RenderBody()", html);
            bodyWithLayout = viewEngine.GetHtml(bodyWithLayout, viewModel,this.User);
            return new HtmlResponse(bodyWithLayout);
        }

        protected void SignIn(string userId)
        {
            this.Request.SessionData["UserId"] = userId;

        }

        protected void SignOut()
        {
            this.Request.SessionData["UserId"] = null;
        }
        protected bool IsUserLoggedIn()
        {
            return this.User != null;
        }

        public string User => this.Request.SessionData.ContainsKey("UserId") ? this.Request.SessionData["UserId"] : null;


    }
}