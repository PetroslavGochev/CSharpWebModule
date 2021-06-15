namespace MyWebServer.Service.Routing
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;

    public interface IRoutingTable
    {
        IRoutingTable Map(string url, HttpMethod method, HttpResponse response);

        IRoutingTable MapGet(string url, HttpResponse resposne);

        HttpResponse MatchRequest(HttpRequest request);
    }
}
