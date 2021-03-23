using System;
using SIS.HTTP.Enumerators;

namespace SIS.MvcFramework
{
    public abstract class HttpMethodAttribute : Attribute
    {
        protected HttpMethodAttribute()
        {

        }

        protected HttpMethodAttribute(string url)
        {
            this.Url = url;
        }

        public string Url { get; }

        public abstract HttpMethodType Type { get; }
    }
}