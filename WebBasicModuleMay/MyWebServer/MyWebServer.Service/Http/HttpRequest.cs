namespace MyWebServer.Service.Http
{
    using MyWebServer.Service.Http.Enums;
    using System;
    using System.Collections.Generic;

    public class HttpRequest
    {

        public HttpMethod Method { get; private set; }

        public string Path { get; private set; }

        public Dictionary<string, HttpHeader> Header 
        { get; } = new Dictionary<string, HttpHeader>();

        public string Body { get; private set; }
    }
}
