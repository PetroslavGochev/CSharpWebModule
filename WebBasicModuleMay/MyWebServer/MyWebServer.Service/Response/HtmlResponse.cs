namespace MyWebServer.Service.Response
{
    using MyWebServer.Service.Results;

    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string html)
            : base(html, "text/html; charset=UTF-8")
        {
        }
    }
}
