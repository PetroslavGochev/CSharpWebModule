using SIS.HTTP.Common;
using SIS.HTTP.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SIS.HTTP.Server
{
    public class HttpServer : IHttpServer
    {
        private readonly TcpListener tcpListener;
        public HttpServer(int port)
        {
            this.tcpListener = new TcpListener(IPAddress.Loopback, port);

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

        public void Stop()
        {
            this.tcpListener.Stop();
        }

        private static async Task ProcessClient(TcpClient tcpClient)
        {
           
            using NetworkStream networkStream = tcpClient.GetStream();
            byte[] requestBytes = new byte[1000000]; // TODO: Use buffer
            int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
            string requestAsString = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);

            var request = new HttpRequest(requestAsString);
            
            string responseText = @"<form method='post'><input name ='username' /><input type ='submit' /><h1>Hello, world!</h1></form>";
            string response = "HTTP/1.0 200 OK" + HttpConstants.NEW_LINE +
                              "Server: SoftUniServer/1.0" + HttpConstants.NEW_LINE +
                              "Content-Type: text/html" + HttpConstants.NEW_LINE +
                              // "Location: https://google.com" + NewLine +
                              // "Content-Disposition: attachment; filename=niki.html" + NewLine +
                              "Content-Lenght: " + responseText.Length + HttpConstants.NEW_LINE +
                              HttpConstants.NEW_LINE +
                              responseText;
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            Console.WriteLine(request);
            Console.WriteLine(new string('=', 60));
            //return networkStream;
        }
    }
}
