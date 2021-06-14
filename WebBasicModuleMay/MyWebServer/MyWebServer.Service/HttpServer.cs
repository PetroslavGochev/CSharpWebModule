namespace MyWebServer.Service
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Routing;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int  port;
        private readonly TcpListener tcpListener;

        public HttpServer(string address, int port, Action<IRoutingTable> routingTable)
        {
            this.ipAddress = IPAddress.Parse(address);
            this.port = port;

            this.tcpListener = new TcpListener(this.ipAddress, port);
        }

        public HttpServer(int port, Action<IRoutingTable> routingTable) :
            this("127.0.0.1", port, routingTable)
        {
        }

        public HttpServer(Action<IRoutingTable> routingTable) :
            this(5000, routingTable)
        {
        }

        public async Task Start()
        {
            this.tcpListener.Start();

            Console.WriteLine($"Server started on port {port}");
            Console.WriteLine($"Listening for request..");

            while (true)
            {
                var connection = await this.tcpListener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var requestText = await ReadRequest(networkStream);

                Console.WriteLine(requestText);

                var request = HttpRequest.Parse(requestText);

                await WriteResponse(networkStream);

                connection.Close();                
            }
        }

        private static async Task WriteResponse(NetworkStream networkStream)
        {
            var content = @"
<html>
    <head>
      <link rel="" icon "" href="" data:,"">
    </head>
    <body>
    Hello from my server!
    </body>
</html>";

            var contentLenght = Encoding.UTF8.GetByteCount(content);

            var response = $@"HTTP/1.1 200 OK
Server: My Web Server
Date: {DateTime.Now}
Content-type: text/html; charset=UTF-8
{content}
";
            var responseBytes = Encoding.UTF8.GetBytes(response);

            await networkStream.WriteAsync(responseBytes);
        }

        private static async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLenght = 1024;

            var totalByte = 0;

            var buffer = new byte[bufferLenght];

            var requestBuilder = new StringBuilder();

            while (networkStream.DataAvailable)
            {
                var byteRead = await networkStream.ReadAsync(buffer, 0, bufferLenght);

                totalByte += byteRead;

                if (totalByte > 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large");
                }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bufferLenght));
            }

            return requestBuilder.ToString();
        }
    }
}
