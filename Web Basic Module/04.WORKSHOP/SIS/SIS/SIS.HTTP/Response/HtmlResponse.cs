using SIS.HTTP.Enumerators;
using SIS.HTTP.Models;
using System.Text;

namespace SIS.HTTP.Response
{
    public class HtmlResponse : HttpResponse
    {
        public HtmlResponse(string html)
            : base()
        {
            this.StatusCode = HttpResponseCode.OK;
            byte[] byteData = Encoding.UTF8.GetBytes(html);
            this.Body = byteData;
            this.Headers.Add(new Header("Content-Type", "text/html"));
            this.Headers.Add(new Header("Content-Type", this.Body.Length.ToString()));
        }
    }
}
