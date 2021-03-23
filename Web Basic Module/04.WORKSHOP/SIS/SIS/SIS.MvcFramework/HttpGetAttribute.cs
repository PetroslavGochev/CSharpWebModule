using System;
using SIS.HTTP;
using SIS.HTTP.Enumerators;

namespace SIS.MvcFramework
{
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute()
        {

        }

        public HttpGetAttribute(string url)
        : base(url)
        {

        }

        public override HttpMethodType Type => HttpMethodType.Get;
    }
}