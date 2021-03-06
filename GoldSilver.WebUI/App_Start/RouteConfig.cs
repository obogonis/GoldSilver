﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GoldSilver.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
            "",
            new
            {
                controller = "Home",
                action = "Index",
                category = (string)null,
                page = 1
            }
            );

            routes.MapRoute("Jeweleries",
                "jewelries",
                new { controller = "Jewelries", action = "List" }
            );

            routes.MapRoute("JeweleryDetails",
                "jewelry/{id}",
                new { controller = "Jewelries", action = "Details" },
                new { id = @"\d+" }
            );

            routes.MapRoute(null,
                "Page/{page}",
                new { controller = "Jewelries", action = "List", category = (string)null },
                new { page = @"\d+" }
            );

            routes.MapRoute(null,
                "{category}",
                new { controller = "Jewelries", action = "List", page = 1 }
            );


            routes.MapRoute(null,
                "{category}/Page/{page}",
                new { controller = "Jewelries", action = "List" },
                new { page = @"\d+" }
            );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}