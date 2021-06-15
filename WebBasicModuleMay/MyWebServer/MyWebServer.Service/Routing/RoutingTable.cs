namespace MyWebServer.Service.Routing
{
    using System;
    using System.Collections.Generic;

    using MyWebServer.Service.Common;
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;
    using MyWebServer.Service.Response;

    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, HttpResponse>> routes;

        public RoutingTable()
        {
            this.routes = new()
            {
                [HttpMethod.Get] = new(),
                [HttpMethod.Post] = new(),
                [HttpMethod.Delete] = new(),
                [HttpMethod.Put] = new(),
            };

        }

        public IRoutingTable Map(string url, HttpMethod method, HttpResponse response)
            => method switch
            {
                HttpMethod.Get => this.MapGet(url, response),
                _ => throw new InvalidOperationException("Invalid method")
            };

        public IRoutingTable MapGet(string url, HttpResponse resposne)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(resposne, nameof(resposne));

            this.routes[HttpMethod.Get][url] = resposne;

            return this;
        }

        public HttpResponse MatchRequest(HttpRequest request)
        {
            var reqestMethod = request.Method;
            var requestUrl = request.Url;

            if (!this.routes.ContainsKey(reqestMethod) 
                || this.routes[reqestMethod].ContainsKey(requestUrl))
            {
                return new NotFoundResponse();
            }

            return this.routes[reqestMethod][requestUrl];
        }
    }
}
