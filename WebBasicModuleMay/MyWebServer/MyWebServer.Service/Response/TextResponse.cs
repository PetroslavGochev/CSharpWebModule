namespace MyWebServer.Service.Results
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Response;

    public class TextResponse : ContentResponse
    {
        public TextResponse(string text)
            : base (text, HttpContentType.PlainText)
        {
        }
    }
}
