namespace MyWebServer.Service.Results
{
    using MyWebServer.Service.Response;

    public class TextResponse : ContentResponse
    {
        public TextResponse(string text)
            : base (text, "text/plain; charset=UTF-8")
        {
        }
    }
}
