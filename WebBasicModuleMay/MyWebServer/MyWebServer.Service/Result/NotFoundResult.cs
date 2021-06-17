namespace MyWebServer.Service.Results
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;
    using MyWebServer.Service.Result;

    public class NotFoundResult : ActionResult
    {
        public NotFoundResult(HttpResponse response)
              : base(response)
        {
            this.StatusCode = HttpStatusCode.NotFound;
        }
    }
}
