namespace MyWebServer.Service.Response
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;

    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse()
              : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
