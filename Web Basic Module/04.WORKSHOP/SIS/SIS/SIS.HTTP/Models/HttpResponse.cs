using SIS.HTTP.Common;
using SIS.HTTP.Enumerators;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Models
{
    public class HttpResponse
    {
        public HttpResponse(HttpResponseCode statusCode,byte[] body)
        {
            this.Version = HttpVersionType.Http11;
            this.StatusCode = statusCode;
            this.Body = body;
            this.Headers = new List<Header>();
            this.Cookies = new List<ResponseCookie>();

            if(body?.Length != 0)
            {
                this.Headers.Add(new Header("Content-Length", body.Length.ToString()));    
            }
        }
        public HttpVersionType Version { get; set; }

        public HttpResponseCode StatusCode { get; set; }

        public IList<Header> Headers { get; set; }

        public IList<ResponseCookie> Cookies { get; set; }

        public byte[] Body { get; set; }

        public override string ToString()
        {
            StringBuilder responseAsString = new StringBuilder();
            var httpVersionAsString = this.Version switch
            {
               HttpVersionType.Http10 => "HTTP/1.0",
               HttpVersionType.Http11 => "HTTP/1.1",
               HttpVersionType.Http20 => "HTTP/2.0",
                _ => "HTTP/1.1"
            };
            responseAsString.Append($"{httpVersionAsString} {(int)this.StatusCode} {this.StatusCode.ToString()}" + HttpConstants.NEW_LINE);
            foreach (var header in this.Headers)
            {
                responseAsString.Append(header.ToString() + HttpConstants.NEW_LINE);
            }
            foreach (var cookie in this.Cookies)
            {
                responseAsString.Append($"Set-Cookie: " + cookie.ToString() + HttpConstants.NEW_LINE);
            }
            responseAsString.Append(HttpConstants.NEW_LINE);
            return responseAsString.ToString();
        }
    }
}
