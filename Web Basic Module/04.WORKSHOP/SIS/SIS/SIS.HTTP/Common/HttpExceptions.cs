using System;

namespace SIS.HTTP.Common
{
    public class HttpExceptions : Exception
    {
        public HttpExceptions(string message)
            :base(message)
        {
            
        }
    }
}
