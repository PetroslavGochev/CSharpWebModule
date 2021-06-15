namespace MyWebServer.Service.Controllers
{
    using System;

    using MyWebServer.Service.Http;
    using MyWebServer.Service.Routing;

    public static class RoutingTableExtensions
    {

        public static IRoutingTable MapGet<ТController>(
            this IRoutingTable routingTable,
            string path,
            Func<ТController, HttpResponse> controlerFunction)
            where ТController : Controller
                => routingTable.MapGet(path, request =>
                   controlerFunction(CreateController<ТController>(request)));

        public static IRoutingTable MapPost<ТController>(
                   this IRoutingTable routingTable,
                   string path,
                   Func<ТController, HttpResponse> controlerFunction)
                   where ТController : Controller
                       => routingTable.MapPost(path, request =>
                          controlerFunction(CreateController<ТController>(request)));

        public static IRoutingTable MapPut<ТController>(
                  this IRoutingTable routingTable,
                  string path,
                  Func<ТController, HttpResponse> controlerFunction)
                  where ТController : Controller
                      => routingTable.MapPut(path, request =>
                         controlerFunction(CreateController<ТController>(request)));

        public static IRoutingTable MapDelete<ТController>(
                  this IRoutingTable routingTable,
                  string path,
                  Func<ТController, HttpResponse> controlerFunction)
                  where ТController : Controller
                      => routingTable.MapDelete(path, request =>
                         controlerFunction(CreateController<ТController>(request)));

        private static ТController CreateController<ТController>(HttpRequest request) where ТController : Controller
       => (ТController)Activator
            .CreateInstance(typeof(ТController), new[] { request });
    }
}
