namespace MyWebServer.Service.Results
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;
    using MyWebServer.Service.Result;

    public class BadRequestResult : ActionResult
    {
        public BadRequestResult(HttpResponse response)
              : base(response)
        {
        }
    }
}
