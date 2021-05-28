namespace MyWebServer.Service.Http
{
    using MyWebServer.Service.Http.Enums;

    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpHeaderCollection Header
        { get; } = new HttpHeaderCollection();

        public string Body { get; set; }
    }
}
