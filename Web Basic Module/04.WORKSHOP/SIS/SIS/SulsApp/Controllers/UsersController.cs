using SIS.HTTP.Models;
using SIS.MvcFramework;
using SulsApp.Data.Model;
using System;
using System.Net.Mail;

namespace SulsApp.Controllers
{
    public class UsersController : Controller
    { 
        [HttpGet]
        public HttpResponse Login()
        {
            return this.View();
        }
        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin()
        {
            return this.View();
        }
        [HttpGet]
        public HttpResponse Register()
        {
            return this.View();

        }
        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister()
        {
            var username = this.Request.FormData["username"];
            var email = this.Request.FormData["email"];
            var password = this.Request.FormData["password"];
            var confirmPassowrd = this.Request.FormData["confirmPassowrd"];


            if(password != confirmPassowrd)
            {
                return  this.Error("<h1>Password should be the same!</h1>");
            }
            if (IsValid(email))
            {
                return this.Error("<h1>Invalid Email Address!</h1>");
            }
            if (password?.Length < 6 || password?.Length > 20)
            {
                return this.Error("<h1>Pasword should be between 6 and 20 characters.</h1>");
            }
            if (username?.Length < 5 || username?.Length > 20)
            {
                return this.Error("<h1>Username should be between 5 and 20 characters.</h1>");
            }

            var user = new User()
            {
                Email = email,
                Password = this.Hash(password),
                Username = username,
            };

            var db = new SulsAppDbContext();
            db.Users.Add(user);
            db.SaveChanges();

            //TODO: Log In...

            return this.Redirect("/");
        }

        private bool IsValid(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
