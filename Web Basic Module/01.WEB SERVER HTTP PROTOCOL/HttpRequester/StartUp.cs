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

        static async Task Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);
            tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                const string NewLine = "\r\n";
                using NetworkStream networkStream = tcpClient.GetStream();
                byte[] requestBytes = new byte[1000000]; // TODO: Use buffer
                int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
                string request = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);

                string responseText = @"<form action= '/Action/Login' method= 'post'>
<input type= date name = 'date' />
<input type= text name = 'text' />
<input type= password name = 'password' />
<input type= submit name = 'Login' />
</form>";
                string response = "HTTP/1.0 200 OK" + NewLine +
                                  "Server: SoftUniServer/1.0" + NewLine +
                                  "Content-Type: text/html" + NewLine +
                                  // "Location: https://google.com" + NewLine +
                                  // "Content-Disposition: attachment; filename=niki.html" + NewLine +
                                  "Content-Lenght: " + responseText.Length + NewLine +
                                  NewLine +
                                  responseText;
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                Console.WriteLine(request);
                Console.WriteLine(new string('=', 60));
            }
        }

    }
}


