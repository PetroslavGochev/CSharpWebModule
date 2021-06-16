namespace MyWebServer.Service.Response
{
    using System.Text;

    using MyWebServer.Service.Common;
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;

    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string content, string contentType)
            : base(HttpStatusCode.Ok)
        {
            this.PrepareContent(content, contentType);
        }
    }
}
