using SIS.HTTP.Enumerators;
using SIS.HTTP.Models;
using SIS.HTTP.Server;
using SIS.MvcFramework;
using SulsApp.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SulsApp
{
    public static class Program
    {
        static async Task Main()
        { 
                await WebHost.StartAsync(new Startup());          
        }
    }
}
