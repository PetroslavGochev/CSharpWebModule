namespace MyWebServer.Service.Controllers
{
    using MyWebServer.Service.Http.Enums;

    public class HttpPostAttribute : HttpMethodAttribute
    {
        public HttpPostAttribute() 
            : base(HttpMethod.Post)
        {
        }
    }
}
