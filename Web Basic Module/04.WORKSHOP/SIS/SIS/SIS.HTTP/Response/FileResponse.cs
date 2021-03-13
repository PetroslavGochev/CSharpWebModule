using SIS.HTTP.Enumerators;
using SIS.HTTP.Models;
using System.Text;

namespace SIS.HTTP.Response
{
    public class FileResponse : HtmlResponse
    {
        public FileResponse(byte[] fileContent, string contentType)
            :base(contentType)
        {
            this.StatusCode = HttpResponseCode.OK;
            this.Body = fileContent;
            this.Headers.Add(new Header("Content-Type", contentType));
            this.Headers.Add(new Header("Content-Type", this.Body.Length.ToString()));
        }
    }
}
