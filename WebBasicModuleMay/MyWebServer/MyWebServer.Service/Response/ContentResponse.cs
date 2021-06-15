namespace MyWebServer.Service.Response
{
    using System.Text;

    using MyWebServer.Service.Common;
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;

    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string text, string contentType)
            : base(HttpStatusCode.Ok)
        {
            Guard.AgainstNull(text);

            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();

            this.Headers.Add("Content-Type", contentType);
            this.Headers.Add("Content-Length", contentLength);

            this.Content = text;
        }
    }
}
