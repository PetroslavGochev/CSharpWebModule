using SIS.HTTP.Enumerators;
using System;

namespace SIS.HTTP.Models
{
    public class Route
    {
        public Route(string path, Func<HttpRequest, HttpResponse> action, HttpMethodType httpMethod)
        {
            this.Path = path;
            this.HttpMethod = httpMethod;
            this.Action = action;
        }
        public string Path { get; set; }

        public HttpMethodType HttpMethod { get; set; }

        public Func<HttpRequest,HttpResponse> Action { get; set; }
    }
}
