using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpRequester
{
    class StartUp
    {

        static Dictionary<string, int> SessionStore = new Dictionary<string, int>();
        static async Task Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 1234);
            tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Run(() => ProcessClientAsync(tcpClient));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        private static async Task ProcessClientAsync(TcpClient tcpClient)
        {
            const string NewLine = "\r\n";
            using NetworkStream networkStream = tcpClient.GetStream();
            byte[] requestBytes = new byte[1000000]; // TODO: Use buffer
            int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
            string request = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);

            var sid = Regex.Match(request, @"sid=[^\n]*\n").Value?.Replace("sid=", string.Empty).Trim();
            Console.WriteLine(sid);
            var newSid = Guid.NewGuid().ToString();
            var count = 0;
            if (SessionStore.ContainsKey(sid))
            {
                SessionStore[sid]++;
                count = SessionStore[sid];
            }
            else
            {
                sid = null;
                SessionStore[newSid] = 1;
                count = 1;
            }


            string responseText = "<h1>" + "Petroslav Ivov Gochev" + "</h1>" + "<h1>" + "Rosen Stanimirov Bobchev"+ "</h1>";
            string response = "HTTP/1.0 301 OK" + NewLine +
                              "Server: SoftUniServer/1.0" + NewLine +
                              "Content-Type: text/html" + NewLine +
                              "Set-Cookie: user=Petroslav; Max-Age: 2500; HttpOnly;" + NewLine +
                              (string.IsNullOrWhiteSpace(sid) ?
                                ("Set-Cookie: sid=" + newSid + NewLine)
                                : string.Empty) +
                               //"Location: https://google.com" + NewLine +
                              // "Content-Disposition: attachment; filename=niki.html" + NewLine +
                              "Content-Lenght: " + responseText.Length + NewLine +
                              NewLine +
                              responseText;
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            Console.WriteLine(request);
            Console.WriteLine(response.Length);
            Console.WriteLine(new string('=', 60));
        }
    }
}


