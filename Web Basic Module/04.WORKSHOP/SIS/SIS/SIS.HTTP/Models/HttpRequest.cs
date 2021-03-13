using SIS.HTTP.Common;
using SIS.HTTP.Enumerators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SIS.HTTP.Models
{
    public class HttpRequest
    {
        public HttpRequest(string httpRequestAsString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
            //this.SessionData = new Dictionary<string, string>();

            var lines = httpRequestAsString
                .Split(new string[] { HttpConstants.NEW_LINE }, StringSplitOptions.None);

            var httpHeaderInfo = lines[0].Split(' ');
          
            if(httpHeaderInfo.Length != 3)
            {
                throw new HttpExceptions("Invalid HTTP header line");
            }

            var httpMethod = httpHeaderInfo[0];

            this.Method = httpMethod switch
            {
                "GET" => HttpMethodType.Get,
                "POST" => HttpMethodType.Post,
                "PUT" => HttpMethodType.Put,
                "DELETE" => HttpMethodType.Delete,
                _ => HttpMethodType.Unknown
            };

            this.Path = httpHeaderInfo[1];

            var httpVersion = httpHeaderInfo[2];
            this.Version = httpVersion switch
            {
                "HTTP/1.0" => HttpVersionType.Http10,
                "HTTP/1.1" => HttpVersionType.Http11,
                "HTTP/2.0" => HttpVersionType.Http20,
                _ => HttpVersionType.Http11
            };

            bool IsHeader = true;
            StringBuilder bb = new StringBuilder();
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    IsHeader = false;
                    continue;
                }
                if (IsHeader)
                {
                    var headerInfo = lines[i]
                        .Split(": ".ToCharArray(), 2, StringSplitOptions.None);
                    if(headerInfo.Length != 2)
                    {
                        throw new HttpExceptions($"Invalid header: {lines[i]}");
                    }
                    var haeder = new Header(headerInfo[0], headerInfo[1]);
                    this.Headers.Add(haeder);

                    if(headerInfo[0] == "Cookie")
                    {
                        var cookieAsString = headerInfo[1];
                        var cookies = cookieAsString
                            .Split("; ".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);

                        foreach (var cookie in cookies)
                        {
                            var cookieParts = cookie.Split("=".ToCharArray(), 2);
                            if(cookieParts.Length == 2)
                            {
                                this.Cookies.Add(new Cookie(cookieParts[0], cookieParts[1]));
                            }
                        }
                    }
                }
                else
                {
                    bb.AppendLine(lines[i]);
                }
            }
            if (!String.IsNullOrEmpty(bb.ToString()))
            {
                this.Body = bb.ToString();
            }
        }
        public HttpMethodType Method { get; set; }

        public string Path { get; set; }

        public HttpVersionType Version { get; set; }

        public IList<Header> Headers { get; set; }

        public IList<Cookie> Cookies { get; set; }

        public IDictionary<string,string> SessionData { get; set; }
        public string Body { get; set; }

    }
}
