namespace MyWebServer.Service.Http
{
    using System.Collections.Generic;

    public class HttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public int Count
            => this.headers.Count;

        public void Add(HttpHeader httpHeader)
            => this.headers.Add(httpHeader.Name, httpHeader);

    }
}
