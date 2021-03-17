using SIS.HTTP.Models;
using SIS.HTTP.Response;
using System.IO;
using System.Runtime.CompilerServices;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {
        private const string RENDER_BODY = "@RenderBody()";
        private const string VIEWS = "Views/";
        private const string LAYOUT = "Shared/_Layout";
        private const string HTML = ".html";
        private const string CONTROLLER = "Controller";
        protected HttpResponse View([CallerMemberName]string viewName = null)
        {
            var controllerName = this.GetType().Name.Replace(CONTROLLER, string.Empty);
            var layout = File.ReadAllText(VIEWS + LAYOUT + HTML);
            var html = File.ReadAllText(VIEWS + controllerName + "/" + viewName + HTML);
            var bodyWithLayout = layout.Replace(RENDER_BODY, html);
            return new HtmlResponse(bodyWithLayout);
        }
    }
}