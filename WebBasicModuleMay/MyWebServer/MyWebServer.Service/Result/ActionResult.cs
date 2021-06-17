namespace MyWebServer.Service.Result
{
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;
    using System.Collections.Generic;

    public abstract class ActionResult : HttpResponse
    {
        protected ActionResult(
            HttpResponse response) 
            : base(response.StatusCode)
        {
            this.Content = response.Content;
            this.PrepareHeaders(response.Headers);
            this.PrepareCookies(response.Cookies);
        }

        protected HttpResponse Response { get; private init; }

        private void PrepareHeaders(IDictionary<string, HttpHeader> headers)
        {
            foreach (var header in headers.Values)
            {
                this.AddHeader(header.Name, header.Value);
            }
        }

        private void PrepareCookies(IDictionary<string, HttpCookie> cookies)
        {
            foreach (var cookie in cookies.Values)
            {
                this.AddHeader(cookie.Name, cookie.Value);
            }
        }
    }
}
