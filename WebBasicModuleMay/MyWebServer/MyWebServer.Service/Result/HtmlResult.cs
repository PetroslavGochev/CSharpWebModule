namespace MyWebServer.Service.Results
{
    using MyWebServer.Service.Http;

    public class HtmlResult : ContentResult
    {
        public HtmlResult(
            HttpResponse response,
            string html)
            : base(response, html, HttpContentType.HtmlText)
        {
        }
    }
}
