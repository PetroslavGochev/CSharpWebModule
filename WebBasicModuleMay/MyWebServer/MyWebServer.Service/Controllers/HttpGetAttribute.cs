namespace MyWebServer.Service.Controllers
{
    using MyWebServer.Service.Http.Enums;

    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute() 
            : base(HttpMethod.Get)
        {
        }
    }
}
