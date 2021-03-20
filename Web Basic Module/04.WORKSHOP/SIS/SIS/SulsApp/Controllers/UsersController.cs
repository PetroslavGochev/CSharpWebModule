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
        public HttpResponse Login(HttpRequest httpRequest)
        {
            return this.View();
        }
        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin(HttpRequest httpRequest)
        {
            return this.View();
        }
        [HttpGet]
        public HttpResponse Register(HttpRequest httpRequest)
        {
            return this.View();

        }
        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister(HttpRequest httpRequest)
        {
            return this.View();

        }

    }
}
