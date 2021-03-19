using SIS.HTTP.Models;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using System;
using System.IO;
using System.Net.Mail;

namespace SulsApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly object usersService;

        public HttpResponse Login(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse Register(HttpRequest request)
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {

            if (input.Password != input.ConfirmedPassword)
            {
                return this.Error("Passwords should be the same.");
            }

            if (input.Username?.Length < 5 || input.Username?.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters.");
            }
            if (input.Password?.Length < 6 || input.Password?.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters.");
            }

            if (!this.IsValid(input.Email))
            {
                return this.Error("Invalid email address.");
            }

            if (this.usersService.IsUsernameUsed(input.Username))
            {
                return this.Error("This username already exist!");
            }

            if (this.usersService.IsEmailUsed(input.Email))
            {
                return this.Error("This email already used!");
            }

            this.usersService.CreateUser(input.Username, input.Email, input.Password);

            this.logger.Log("New user: " + input.Username);

            return this.Redirect("/Users/Login");

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
