using SIS.HTTP.Enumerators;

namespace SIS.HTTP.Response
{
    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string newLocation)
        {
            this.Headers.Add(new Header("Location", newLocation));
            this.StatusCode = HttpResponseCode.FOUND;
        }
    }
}