namespace MyWebServer.Service.Response
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Results;

    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string html)
            : base(html, HttpContentType.HtmlText)
        {
        }
    }
}
