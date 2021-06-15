namespace MyWebServer.Service.Response
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;

    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string location) 
            : base(HttpStatusCode.Found)
        {
        }
    }
}
