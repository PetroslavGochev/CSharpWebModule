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
            return this.View();

        }

    }
}
