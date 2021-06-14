namespace MyWebServer.Service.Results
{
    using MyWebServer.Service.Common;
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;
    using System.Text;

    public class TextResponse : HttpResponse
    {
        public TextResponse(string text, string contentType)
            :base(HttpStatusCode.Ok)
        {
            Guard.AgainstNull(text);

            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();

            this.Headers.Add("Content-Type", contentType);
            this.Headers.Add("Content-Length", contentLength);

            this.Content = text;
        }
        public TextResponse(string text)
            : this(text, "text/plain; charset=UTF-8")
        {
        }
    }
}
