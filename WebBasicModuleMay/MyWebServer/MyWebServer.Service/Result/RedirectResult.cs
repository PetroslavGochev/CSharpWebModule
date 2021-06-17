namespace MyWebServer.Service.Results
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;
    using MyWebServer.Service.Result;

    public class RedirectResult : ActionResult
    {
        public RedirectResult(
            HttpResponse response, 
            string location) 
            : base(response)
        {
            this.StatusCode = HttpStatusCode.Found;
           this.AddHeader(HttpHeader.Location,location);
        }
    }
}
