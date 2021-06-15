namespace MyWebServer.Service.Http
{
    using MyWebServer.Service.Common;
    using MyWebServer.Service.Http.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HttpRequest
    {
        public HttpMethod Method { get; private set; }

        public string Path { get; private set; }

        public Dictionary<string, string> Query { get; private set; }

        public HttpHeaderCollection Header { get; private set; }

        public string Body { get; private set; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(GlobalConstants.NEW_LINE);

            var startLine = lines.First().Split(" ");

            var method = HttpMethodParse(startLine[0]);

            var url = startLine[1];

            var (path, query) = ParseUrl(url);

            var headers = HttpParseHeaderCollection(lines.Skip(1));

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join(string.Empty, bodyLines);

            return new HttpRequest
            {
                Method = method,
                Path = path,
                Header = headers,
                Body = body,
            };
        }

        // /Cats?name=Ivan&Age=5
        private static (string, Dictionary<string, string>) ParseUrl(string url)
        {
            var urlParts = url.Split("?");

            var path = urlParts[0];
            var query = urlParts.Length > 1 
                ? ParseQuery(urlParts[1])
                : new Dictionary<string, string>();

            return (path, query);
        }

        private static Dictionary<string, string> ParseQuery(string queryString)       
               => queryString
                    .Split('&')
                    .Select(part => part.Split("="))
                    .Where(part => part.Length == 2)
                    .ToDictionary(part => part[0], part => part[1]);
           

        private static HttpHeaderCollection HttpParseHeaderCollection(IEnumerable<string> headerLines)
        {
            var headerCollection = new HttpHeaderCollection();

            foreach (var headerLine in headerLines)
            {
                if (headerLine == string.Empty)
                {
                    break;
                }

                var headerParts = headerLine.Split(":", 2);

                if (headerParts.Length != 2)
                {
                    throw new InvalidOperationException("Invalid Request");
                }

                var headerName = headerParts[0];
                var headerValue = headerParts[1].Trim();

                var header = new HttpHeader(headerName, headerValue);

                headerCollection.Add(headerName, headerValue);
            }

            return headerCollection;
        }

        private static HttpMethod HttpMethodParse(string method)
        {
            return method.ToUpper() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => throw new InvalidOperationException("Invalid method")
            };
        }
    }
}
