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

        public HttpResponse Login()
        {
            // var user = this.db.Users.Find(username, password);
            // 
            // if (user != null)
            // {
            //     this.SignIn(user.Id);
            //
            //     return Text("User authenticated!");
            // } 
            // 
            // return Text("Invalid credentials!");

            var someUserId = "MyUserId"; // should come from the database

            this.SignIn(someUserId);

            return Text("User authenticated!");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return Text("User signed out!");
        }

        public HttpResponse AuthenticationCheck()
        {
            if (this.User.IsAuthenticated)
            {
                return Text($"Authenticated user: {this.User.Id}");
            }

            return Text("User is not authenticated!");
        }

        [Authorize]
        public HttpResponse AutorizhedCheck()
            => this.Text($"Current user: {this.User.Id}");

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
