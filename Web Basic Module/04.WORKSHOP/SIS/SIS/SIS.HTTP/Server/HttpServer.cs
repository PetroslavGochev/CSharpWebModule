using SIS.HTTP.Common;
using SIS.HTTP.Enumerators;
using SIS.HTTP.Logging;
using SIS.HTTP.Models;
using SIS.HTTP.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

//namespace SIS.HTTP.Server
//{
//    public class HttpServer : IHttpServer
//    {
//        private readonly TcpListener tcpListener;
//        private readonly IList<Route> routeTable;
//        private readonly ILogger logger;
//        private readonly IDictionary<string, IDictionary<string, string>> session;

//        public HttpServer(int port, IList<Route> routeTable, ILogger logger)
//        {
//            this.tcpListener = new TcpListener(IPAddress.Loopback, port);
//            this.routeTable = routeTable;
//            this.logger = logger;
//            this.session = new Dictionary<string, IDictionary<string, string>>();
//        }
//        public async Task ResetAsync()
//        {
//            this.Stop();
//            await this.StartAsync();
//        }

//        public async Task StartAsync()
//        {
//            tcpListener.Start();
//            while (true)
//            {
//                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
//#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
//                Task.Run(() => ProcessClientAsync(tcpClient));
//#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
//            }
//        }

//        private async Task ProcessClientAsync(TcpClient tcpClient)
//        {
//            using NetworkStream networkStream = tcpClient.GetStream();
//            try
//            {
//                byte[] requestBytes = new byte[1000000]; // TODO: Use buffer
//                int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
//                string requestAsString = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);

//                var request = new HttpRequest(requestAsString);
//                var sessionCookie = request.Cookies.FirstOrDefault(c => c.Name == HttpConstants.COOKIE_NAME);
//                string newSessionId = null;


//                if (sessionCookie != null && this.session.ContainsKey(sessionCookie.Value))
//                {
//                    request.SessionData = this.session[sessionCookie.Value];
//                }
//                else
//                {
//                    newSessionId = Guid.NewGuid().ToString();
//                    var dictionary = new Dictionary<string, string>();
//                    this.session.Add(newSessionId, dictionary);
//                    request.SessionData = dictionary;

//                }

//                var route = this.routeTable
//                    .FirstOrDefault(rt => rt.HttpMethod  == request.Method && string.Compare(rt.Path,request.Path,true) == 0);
//                HttpResponse httpResponse;

//                if (route == null)
//                {
//                    httpResponse = new HttpResponse(HttpResponseCode.NOTFOUND, new byte[0]);
//                }
//                else
//                {
//                    httpResponse = route.Action(request);
//                }

//                httpResponse.Headers.Add(new Header("Server", "SoftUniServer/1.0"));


//                if (newSessionId != null)
//                {
//                    httpResponse.Cookies.Add(new ResponseCookie(HttpConstants.COOKIE_NAME, Guid.NewGuid().ToString())
//                    {
//                        MaxAge = 30 * 3600,
//                        HttpOnly = true,
//                        Secure = true
//                    });
//                }


//                byte[] responseBytes = Encoding.UTF8.GetBytes(httpResponse.ToString());
//                await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
//                await networkStream.WriteAsync(httpResponse.Body, 0, httpResponse.Body.Length);

//                this.logger.Log($"{request.Method} - {request.Path}");
//            }
//            catch (Exception ex)
//            {
//                var errorResponse = new HttpResponse(HttpResponseCode.INTERNALSERVERERROR, Encoding.UTF8.GetBytes(ex.Message));
//                errorResponse.Headers.Add(new Header("Content-type", "text/html"));

//                byte[] responseBytes = Encoding.UTF8.GetBytes(errorResponse.ToString());
//                await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
//                await networkStream.WriteAsync(errorResponse.Body, 0, errorResponse.Body.Length);
//            }

//        }

//        public void Stop()
//        {
//            this.tcpListener.Stop();
//        }

//    }
//}

//using SIS.HTTP.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;

namespace SIS.HTTP
{
    public class HttpServer : IHttpServer
    {
        private readonly TcpListener tcpListener;
        private readonly IList<Route> routeTable;
        private readonly ILogger logger;
        private readonly IDictionary<string, IDictionary<string, string>> sessions;

        public HttpServer(int port, IList<Route> routeTable, ILogger logger)
        {
            this.tcpListener = new TcpListener(IPAddress.Loopback, port);
            this.routeTable = routeTable;
            this.logger = logger;
            this.sessions = new Dictionary<string, IDictionary<string, string>>();
        }
        public async Task StartAsync()
        {
            this.tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Run(() => ProcessClientAsync(tcpClient));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        public async Task ResetAsync()
        {
            this.Stop();
            await this.StartAsync();
        }

        public void Stop()
        {
            this.tcpListener.Stop();
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            using NetworkStream networkStream = tcpClient.GetStream();
            try
            {
                byte[] requestBytes = new byte[1000000]; //TODO: Use buffer

                int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
                string requestAsString = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);

                var request = new HttpRequest(requestAsString);
                var sessionCookie = request.Cookies.FirstOrDefault(x => x.Name == HttpConstants.COOKIE_NAME);
                string newSessionId = null;
                if (sessionCookie != null && this.sessions.ContainsKey(sessionCookie.Value))
                {
                    request.SessionData = this.sessions[sessionCookie.Value];
                }
                else
                {
                    newSessionId = Guid.NewGuid().ToString();
                    var dictionary = new Dictionary<string, string>();
                    this.sessions.Add(newSessionId, dictionary);
                    request.SessionData = dictionary;
                }

                Console.WriteLine($"{request.Method} {request.Path}");

                var route = this.routeTable.FirstOrDefault(
                    x => x.HttpMethod == request.Method && string.Compare(x.Path, request.Path, true) == 0);
                HttpResponse response;

                if (route == null)
                {
                    response = new HttpResponse(HttpResponseCode.NOTFOUND, new byte[0]);
                }
                else
                {
                    response = route.Action(request);
                }

                response.Headers.Add(new Header("Server", "SoftUniServer/1.0"));

                if (newSessionId != null)
                {
                    response.Cookies.Add(new ResponseCookie(HttpConstants.COOKIE_NAME, newSessionId)
                    { HttpOnly = true, MaxAge = 30 * 3600 });
                }

                byte[] responseBytes = Encoding.UTF8.GetBytes(response.ToString());
                await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                await networkStream.WriteAsync(response.Body, 0, response.Body.Length);

                Console.WriteLine(new string('=', 60));

            }

            catch (Exception ex)
            {
                var errorResponse = new HttpResponse(HttpResponseCode.INTERNALSERVERERROR,
                    Encoding.UTF8.GetBytes(ex.ToString()));
                errorResponse.Headers.Add(new Header("Content-Type", "text/plain"));
                byte[] responseBytes = Encoding.UTF8.GetBytes(errorResponse.ToString());
                await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                await networkStream.WriteAsync(errorResponse.Body, 0, errorResponse.Body.Length);
            }
        }
    }
}
