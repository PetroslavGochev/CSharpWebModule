using SIS.HTTP.Models;
using SIS.MvcFramework;
using SulsApp.Data.Model;
using SulsApp.Services;
using System;
using System.Net.Mail;

namespace SulsApp.Controllers
{   
    public class UsersController : Controller
    {
        private IUserService userService;
        private readonly ILogger logger;

        public UsersController(IUserService userService,ILogger logger)
        {
            this.userService = userService;
            this.logger = logger;
        }
        [HttpGet]
        public HttpResponse Login()
        {
            return this.View();
        }
        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin()
        {
            string username = this.Request.FormData["username"];
            string password = this.Request.FormData["password"];

            var userId = this.userService.GetUserId(username, password);

            if(userId == null)
            {
                return this.Redirect("/Users/Login");
            }
            this.SignIn(userId);
            this.logger.Log("User logged in: " + username);
            return this.Redirect("/");
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
            this.userService.CreateUser(username, email, password);
            this.logger.Log("New user: " + username);
          
            return this.Redirect("/Users/Login");
        }

        public HttpResponse LogOut()
        {
            this.SignOut();
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
