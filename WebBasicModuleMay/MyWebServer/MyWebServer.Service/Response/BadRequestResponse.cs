namespace MyWebServer.Service.Response
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;

    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse()
              : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
