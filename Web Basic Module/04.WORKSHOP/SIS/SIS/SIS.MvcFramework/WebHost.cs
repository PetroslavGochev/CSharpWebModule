using SIS.HTTP.Enumerators;
using SIS.HTTP.Models;
using SIS.HTTP.Response;
using SIS.HTTP.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace SIS.MvcFramework
{
    public static class WebHost
    {
        public static async Task StartAsync(IMvcApplication application)
        {
            var routeTable = new List<Route>();
            application.ConfigureServices();
            application.Configure(routeTable);
            AutoRegisterActionRoutes(routeTable, application);
            AutoRegisterRoutes(routeTable, application);
            foreach (var route in routeTable)
            {
                Console.WriteLine(route);
            }

            var httpServer = new HttpServer(80, routeTable);
            await httpServer.StartAsync();
        }
        private static void AutoRegisterActionRoutes(IList<Route> routeTable, IMvcApplication application)
        {
            var controllers = application.GetType().Assembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Controller)) && !type.IsAbstract);
            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods()
                    .Where(x => !x.IsSpecialName
                                && !x.IsConstructor
                                && x.IsPublic
                                && x.DeclaringType == controller);
                foreach (var action in actions)
                {

                    string url = "/" + controller.Name.Replace("Controller", string.Empty) + "/" + action.Name;
                    var attribute = action.GetCustomAttributes()
                            .FirstOrDefault(x => x.GetType()
                                .IsSubclassOf(typeof(HttpMethodAttribute)))
                        as HttpMethodAttribute;
                    var httpActionType = HttpMethodType.Get;
                    if (attribute != null)
                    {
                        httpActionType = attribute.Type;
                        if (attribute.Url != null)
                        {
                            url = attribute.Url;
                        }
                    }
                    routeTable.Add(new Route(httpActionType, url, (request) => InvokeAction(request, controller, action)));
                }

            }
        }
        private static HttpResponse InvokeAction(HttpRequest request, Type controllerType, MethodInfo action)
        {
            var controller = Activator.CreateInstance(controllerType) as Controller;
            controller.Request = request;
            var response = action.Invoke(controller, new object[] { request }) as HttpResponse;
            return response;
        }

        private static void AutoRegisterRoutes(List<Route> routeTable, IMvcApplication application)
        {
            var staticFiles = Directory.GetFiles("wwwroot", "*", SearchOption.AllDirectories);

            foreach (var staticFile in staticFiles)
            {
                var path = staticFile.Replace("wwwroot", string.Empty).Replace("\\", "/");
                routeTable.Add(new Route(HttpMethodType.Get, path, (request) =>
                {
                    var fileInfo = new FileInfo(staticFile);
                    var contentType = fileInfo.Extension switch
                    {
                        ".css" => "text/css",
                        ".html" => "text/html",
                        ".js" => "text/javascript",
                        ".ico" => "image/x-icon",
                        ".jpg" => "image/jpeg",
                        ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        _ => "text/plain",
                    };
                    return new FileResponse(File.ReadAllBytes(staticFile), contentType);

                }));
            }
        }
    }
}



