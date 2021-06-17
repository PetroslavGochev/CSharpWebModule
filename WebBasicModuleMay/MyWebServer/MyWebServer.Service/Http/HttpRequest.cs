namespace MyWebServer.Service.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyWebServer.Service.Common;
    using MyWebServer.Service.Http.Enums;

    public class HttpRequest
    {
        private static Dictionary<string, HttpSession> Sessions
            => new();

        public HttpMethod Method { get; private set; }

        public string Path { get; private set; }

        public IReadOnlyDictionary<string, string> Query { get; private set; }

        public IReadOnlyDictionary<string, string> Form { get; private set; }

        public IReadOnlyDictionary<string, HttpHeader> Headers { get; private set; }

        public IReadOnlyDictionary<string, HttpCookie> Cookies { get; set; }

        public HttpSession Session { get; set; }

        public string Body { get; private set; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(GlobalConstants.NEW_LINE);

            var startLine = lines.First().Split(" ");

            var method = MethodParse(startLine[0]);

            var url = startLine[1];

            var (path, query) = ParseUrl(url);

            var headers = ParseHeaderCollection(lines.Skip(1));

            var cookies = ParseCookies(headers);

            var session = GetSession(cookies);

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join(string.Empty, bodyLines);

            var form = ParseForm(headers, body);

            return new HttpRequest
            {
                Method = method,
                Path = path,
                Headers = headers,
                Cookies = cookies,
                Session = session,
                Query = query,
                Body = body,
            };
        }

        public override string ToString()
        {
            return base.ToString();
        }

        private static HttpSession GetSession(Dictionary<string, HttpCookie> cookies)
        {
            var sessionId = cookies.ContainsKey(HttpSession.SessionCookieName)
                ? cookies[HttpSession.SessionCookieName].Value
                : Guid.NewGuid().ToString();

            if (!Sessions.ContainsKey(sessionId))
            {
                Sessions[sessionId] = new HttpSession(sessionId);
            }

            return Sessions[sessionId];
        }

        private static Dictionary<string, string> ParseForm(Dictionary<string, HttpHeader> headers, string body)
        {
            var result = new Dictionary<string, string>();
            if (headers.ContainsKey(HttpHeader.ContentType)
                 && headers[HttpHeader.ContentType].Value == HttpContentType.FormUrlEncoded)
            {
                result = ParseQuery(body);
            }

            return result;
        }

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


        private static Dictionary<string, HttpHeader> ParseHeaderCollection(IEnumerable<string> headerLines)
        {
            var headerCollection = new Dictionary<string, HttpHeader>();

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

                headerCollection.Add(headerName, new HttpHeader(headerName, headerValue));
            }

            return headerCollection;
        }

        private static Dictionary<string, HttpCookie> ParseCookies(Dictionary<string, HttpHeader> headers)
        {
            var cookieCollection = new Dictionary<string, HttpCookie>();

            if (headers.ContainsKey(HttpHeader.Cookie))
            {
                var cookieHeader = headers[HttpHeader.Cookie];

                var allCookies = cookieHeader
                    .Value
                    .Split(';')
                    .Select(c => c.Split('='));

                foreach (var cookieParts in allCookies)
                {
                    var cookieName = cookieParts[0].Trim();
                    var cookieValue = cookieParts[1].Trim();

                    cookieCollection.Add(cookieName, new HttpCookie(cookieName, cookieValue));
                }
            }

            return cookieCollection;
        }

        private static HttpMethod MethodParse(string method)
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
