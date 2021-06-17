namespace MyWebServer.Controllers
{
    using System;

    using MyWebServer.Service.Controllers;
    using MyWebServer.Service.Http;
    using MyWebServer.Service.Result;

    public class AccountController : Controller
    {
        public AccountController(HttpRequest request) 
            : base(request)
        {
        }

        public ActionResult ActionWithCookies()
        {
            const string cookieName = "My-Cookie";

            if (this.Request.Cookies.ContainsKey(cookieName))
            {
                var cookie = this.Request.Cookies[cookieName];

                return this.Text($"Cookies already exist - {cookie}");
            }

            this.Response.AddCookie("My Cookie", "Value");
            this.Response.AddCookie("My Second Cookie", "Second Value");
            return this.Text("Text");
        }

        public ActionResult ActionWithSession
            ()
        {
            const string currentDateKey = "CurrentDate";

            if (this.Request.Session.ContainsKey(currentDateKey))
            {
                var cuurentDate = this.Request.Session[currentDateKey];

                return this.Text($"Stored date: {cuurentDate}");
            }

            this.Request.Session[currentDateKey] = DateTime.UtcNow.ToString();
            return this.Text("Current date stored");
        }
    }
}
