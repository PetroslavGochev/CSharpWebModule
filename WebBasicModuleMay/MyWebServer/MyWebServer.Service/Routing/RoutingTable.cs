namespace MyWebServer.Service.Routing
{
    using System;
    using System.Collections.Generic;

    using MyWebServer.Service.Common;
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;
    using MyWebServer.Service.Results;

    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, Func<HttpRequest, HttpResponse>>> routes;

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
        public IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunction)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(responseFunction, nameof(responseFunction));

            this.routes[HttpMethod.Get][path.ToLower()] = responseFunction;

            return this;
        }

        public IRoutingTable Map(
            HttpMethod method,
            string path,
            HttpResponse response)
        {
            Guard.AgainstNull(response, nameof(response));

            return this.Map(method, path, request => response);
        }

        public IRoutingTable MapGet(string path, HttpResponse response)
            => this.MapGet(path, request => response);

        public IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunction)
            => this.Map(HttpMethod.Get, path, responseFunction);

        public IRoutingTable MapPut(string path, HttpResponse response)
            => this.MapPut(path, request => response);

        public IRoutingTable MapPut(string path, Func<HttpRequest, HttpResponse> responseFunction)
            => this.Map(HttpMethod.Put, path, responseFunction);

        public IRoutingTable MapDelete(string path, HttpResponse response)
            => this.MapDelete(path, request => response);

        public IRoutingTable MapDelete(string path, Func<HttpRequest, HttpResponse> responseFunction)
            => this.Map(HttpMethod.Delete, path, responseFunction);

        public IRoutingTable MapPost(string path, HttpResponse response)
           => this.MapPost(path, request => response);

        public IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunction)
            => this.Map(HttpMethod.Get, path, responseFunction);

        public HttpResponse ExecuteRequest(HttpRequest request)
        {
            var reqestMethod = request.Method;
            var requestPath = request.Path.ToLower();

            if (!this.routes.ContainsKey(reqestMethod) 
                || this.routes[reqestMethod].ContainsKey(requestPath))
            {
                return new HttpResponse(HttpStatusCode.NotFound);
            }

            var responseFunction = this.routes[reqestMethod][requestPath];

            return responseFunction(request);
        }
    }
}
