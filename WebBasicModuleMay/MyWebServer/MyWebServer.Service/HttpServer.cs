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
        private readonly IRoutingTable routingTable;

        public HttpServer(string address, int port, Action<IRoutingTable> routingTableConfiguration)
        {
            this.ipAddress = IPAddress.Parse(address);
            this.port = port;

            this.tcpListener = new TcpListener(this.ipAddress, port);

            this.routingTable = new RoutingTable();
            routingTableConfiguration(this.routingTable);
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

                try
                {
                    var request = HttpRequest.Parse(requestText);

                    var response = this.routingTable.ExecuteRequest(request);

                    this.PrepareSession(request, response);

                    this.LogPipeLine(request, response);

                    await WriteResponse(networkStream, response);
                }
                catch (Exception ex)
                {
                    await HandleError(networkStream, ex);
                }

                connection.Close();                
            }
        }

        private void LogPipeLine(HttpRequest request, HttpResponse response)
        {
            var separator = new string('-', 50);

            var log = new StringBuilder();

            log.AppendLine();
            log.AppendLine(separator);

            log.AppendLine("REQUEST:");
            log.AppendLine(request.ToString());

            log.AppendLine();

            log.AppendLine("RESPONSE:");
            log.AppendLine(response.ToString());

            log.AppendLine();

            Console.WriteLine(log);
        }

        private static async Task HandleError(NetworkStream networkStream, Exception ex)
        {
            var errorMessage = $"{ex.Message} {Environment.NewLine} {ex.StackTrace}";
            var errorResponse = HttpResponse.ForError(errorMessage);

            await WriteResponse(networkStream, errorResponse);
        }

        private void PrepareSession(HttpRequest request, HttpResponse response)
        {
            response.AddCookie(HttpSession.SessionCookieName, request.Session.Id);
        }

        private static async Task WriteResponse(
            NetworkStream networkStream,
            HttpResponse response)
        {
            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());

            await networkStream.WriteAsync(responseBytes);
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var totalBytes = 0;

            var requestBuilder = new StringBuilder();

            do
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);

                totalBytes += bytesRead;

                if (totalBytes > 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large.");
                }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);

            return requestBuilder.ToString();
        }
    }
}
