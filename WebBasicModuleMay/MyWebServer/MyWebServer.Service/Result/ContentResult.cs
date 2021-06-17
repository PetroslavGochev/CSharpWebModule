namespace MyWebServer.Service.Results
{
    using System.Text;

    using MyWebServer.Service.Common;
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;
    using MyWebServer.Service.Result;

    public class ContentResult : ActionResult
    {
        public ContentResult(
            HttpResponse response,
            string content,
            string contentType)
            : base(response)
        {
            this.PrepareContent(content, contentType);
        }
    }
}
