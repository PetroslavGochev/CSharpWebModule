namespace MyWebServer.Service.Http
{
    using MyWebServer.Service.Http.Enums;
    using System;
    using System.Text;

    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode StatusCode)
        {
            this.StatusCode = StatusCode;

            this.Headers.Add("Server", "My Web Server");
            this.Headers.Add("Date", $"{DateTime.UtcNow:r}");
        }

        public HttpStatusCode StatusCode { get; set; }

        public HttpHeaderCollection Headers
        { get; } = new HttpHeaderCollection();

        public string Content { get; set; }


        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}");

            foreach (var header in this.Headers)
            {
                result.AppendLine(header.ToString());
            }

            if (!string.IsNullOrEmpty(this.Content))
            {
                result.AppendLine();

                result.Append(this.Content);
            }

            return result.ToString();
        }
    }
}
