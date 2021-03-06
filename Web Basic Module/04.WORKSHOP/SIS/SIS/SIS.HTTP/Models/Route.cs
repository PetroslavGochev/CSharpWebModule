using System;

namespace SIS.HTTP
{
    public class Route
    {
        public Route(string path, HttpMethodType method, Func<HttpRequest, HttpResponse> action)
        {
            this.Path = path;
            this.HttpMethod = method;
            this.Action = action;
        }
        public string Path { get; set; }
        public HttpMethodType HttpMethod { get; set; }
        public Func<HttpRequest, HttpResponse> Action { get; set; }

        public override string ToString()
        {
            return $"{this.HttpMethod} => {this.Path}";
        }
    }
}