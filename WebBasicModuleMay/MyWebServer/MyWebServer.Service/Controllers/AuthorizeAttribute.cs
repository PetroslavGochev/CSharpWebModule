namespace MyWebServer.Service.Controllers
{
    using System;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AutorizeAttribute : Attribute
    {
    }
}
