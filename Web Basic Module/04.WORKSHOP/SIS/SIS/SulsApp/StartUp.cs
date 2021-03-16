﻿using SIS.HTTP.Enumerators;
using SIS.HTTP.Models;
using SIS.HTTP.Server;
using SulsApp.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SulsApp
{
    public static class StartUp
    {
        static async Task Main()
        {
            var db = new SulsAppDbContext();
            db.Database.EnsureCreated();

            var routeTable = new List<Route>();
            routeTable.Add(new Route("/", new HomeController().Index, HttpMethodType.Get));


            var httpServer = new HttpServer(80, routeTable);
            await httpServer.StartAsync();
        }
    }
}
