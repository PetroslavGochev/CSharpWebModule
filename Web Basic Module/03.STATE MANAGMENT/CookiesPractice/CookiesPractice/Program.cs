using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CookiesPractice
{
    class Program
    {
        private static Dictionary<string, Dictionary<DateTime, int>> sessionStore = new Dictionary<string, Dictionary<DateTime, int>>();
        const string NewLine = "\r\n";
        static async Task Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 3333);
            tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                await Task.Run(() => ProcessClient(tcpClient));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            }

        }

        private static async Task ProcessClient(TcpClient tcpClient)
        {
            
            NetworkStream networkStream = tcpClient.GetStream();

            byte[] requestBytes = new byte[1000000]; // TODO: Use buffer
            int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
            string request = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);
            var regex = @"sid=(?<value>.+)";
            var sid = Regex.Match(request, regex);
            var userId = sid.Groups["value"].ToString().Split(";")[0];
            if (!sessionStore.ContainsKey(userId))
            {
                sessionStore.Add(userId, new Dictionary<DateTime, int>());
                sessionStore[userId].Add(DateTime.Today, 1);
            }


            string responseText = @"<h1>" + sessionStore[userId][DateTime.Today].ToString() + ":views" + " " + DateTime.Now + "</h1>";
            string response = "HTTP/1.0 200 OK" + NewLine +
                              "Server: SoftUniServer/1.0" + NewLine +
                              "Content-Type: text/html" + NewLine +
                              // "Location: https://google.com" + NewLine +
                              // "Content-Disposition: attachment; filename=petroslav.html" + NewLine +
                              //"Set-Cookie: language=bg; user=petroslav; path=/; secure; SameSite=Strict" + NewLine +
                              (string.IsNullOrWhiteSpace(userId) ? "Set-Cookie: sid=" + Guid.NewGuid().ToString()+ NewLine : string.Empty) + 
                              "Content-Lenght: " + responseText.Length + NewLine +
                              NewLine +
                              responseText;
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            Console.WriteLine(request);
            Console.WriteLine(new string('=', 60));
            //return networkStream;
        }
    }

}
