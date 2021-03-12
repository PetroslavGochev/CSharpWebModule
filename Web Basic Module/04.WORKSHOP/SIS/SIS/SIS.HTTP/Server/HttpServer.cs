﻿using SIS.HTTP.Common;
using SIS.HTTP.Enumerators;
using SIS.HTTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SIS.HTTP.Server
{
    public class HttpServer : IHttpServer
    {
        private readonly TcpListener tcpListener;
        private readonly IList<Route> routeTable;

        public HttpServer(int port, IList<Route> routeTable)
        {
            this.tcpListener = new TcpListener(IPAddress.Loopback, port);
            this.routeTable = routeTable;
        }
        public async Task ResetAsync()
        {
            this.Stop();
            await this.StartAsync();
        }

        public async Task StartAsync()
        {
            tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Run(() => ProcessClient(tcpClient));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        private async Task ProcessClient(TcpClient tcpClient)
        {
            using NetworkStream networkStream = tcpClient.GetStream();
            try
            {
                byte[] requestBytes = new byte[1000000]; // TODO: Use buffer
                int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
                string requestAsString = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);

                var request = new HttpRequest(requestAsString);


                var route = this.routeTable
                    .FirstOrDefault(rt => rt.HttpMethod == request.Method && rt.Path == request.Path);
                HttpResponse httpResponse;

                if (route == null)
                {
                    httpResponse = new HttpResponse(HttpResponseCode.NOTFOUND, new byte[0]);
                }
                else
                {
                    httpResponse = route.Action(request);
                }


                httpResponse.Headers.Add(new Header("Server", "SoftUniServer/1.0"));

                httpResponse.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
                {
                    MaxAge = 3600,
                    HttpOnly = true,
                    Secure = true
                });

                byte[] responseBytes = Encoding.UTF8.GetBytes(httpResponse.ToString());
                await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                await networkStream.WriteAsync(httpResponse.Body, 0, httpResponse.Body.Length);

                Console.WriteLine($"{request.Method} - {request.Path}");
                Console.WriteLine(new string('=', 60));
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponse(HttpResponseCode.INTERNALSERVERERROR, Encoding.UTF8.GetBytes(ex.Message));
                errorResponse.Headers.Add(new Header("Content-type", "text/html"));

                byte[] responseBytes = Encoding.UTF8.GetBytes(errorResponse.ToString());
                await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                await networkStream.WriteAsync(errorResponse.Body, 0, errorResponse.Body.Length);
            }

        }

        public void Stop()
        {
            this.tcpListener.Stop();
        }

    }
}
