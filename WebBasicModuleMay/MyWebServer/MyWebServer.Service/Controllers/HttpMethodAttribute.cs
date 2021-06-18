namespace MyWebServer.Service.Controllers
{
    using System;

    using MyWebServer.Service.Http.Enums;

    [AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpMethodAttribute : Attribute
    {
        protected HttpMethodAttribute(HttpMethod httpMethod)
        {
            this.HttpMethod = httpMethod;
        }

        public HttpMethod HttpMethod { get; set; }
    }
}
