using SIS.HTTP.Models;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using SulsApp.Services;
using SulsApp.ViewModels;
using System;
using System.IO;

namespace SulsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger logger;

        public HomeController(ILogger logger)
        {
            this.logger = logger;
        }

        [HttpGet("/")]
        public  HttpResponse Index(HttpRequest request)
        {
            var viewModel = new IndexViewModel()
            {
                Message = "Welcome to SULS Platform!",
                Year = DateTime.UtcNow.Year,
            };
            return this.View(viewModel);
        }
    }
}
