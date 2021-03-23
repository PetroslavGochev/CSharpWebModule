using SIS.HTTP.Logging;
using SIS.HTTP.Models;
using SIS.MvcFramework;
using SulsApp.Services;
using SulsApp.ViewModels.Users;
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
        [HttpPost]
        public HttpResponse Login(string username,string password)
        {
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
        [HttpPost]
        public HttpResponse Register(RegisterInputModel inputModel)
        {
            if(inputModel.Password != inputModel.ConfirmPassword)
            {
                return  this.Error("<h1>Password should be the same!</h1>");
            }
            if (inputModel.Password?.Length < 6 || inputModel.Password?.Length > 20)
            {
                return this.Error("<h1>Pasword should be between 6 and 20 characters.</h1>");
            }
            if (inputModel.Username?.Length < 5 || inputModel.Username?.Length > 20)
            {
                return this.Error("<h1>Username should be between 5 and 20 characters.</h1>");
            }
            if (!IsValid(inputModel.Email))
            {
                return this.Error("<h1>Invalid Email Address!</h1>");
            }
            if (this.userService.IsEmailUsed(inputModel.Email))
            {
                return this.Error("<h1>Email already used!</h1>");
            }
            if (this.userService.IsUsernameUsed(inputModel.Username))
            {
                return this.Error("<h1>Username already used!</h1>");
            }
            
            this.userService.CreateUser(inputModel.Username, inputModel.Email, inputModel.Password);
            this.logger.Log("New user: " + inputModel.Username);
          
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
