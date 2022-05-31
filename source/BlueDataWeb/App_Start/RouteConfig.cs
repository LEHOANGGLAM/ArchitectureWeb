using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlueDataWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "BlueDataWeb.Controllers" }
            );

            // Or this route. It should return /Blog/Details/user-group-2013
            routes.MapRoute(
                 name: "MyRoute2",
                 url: "{controller}/{action}/{postName}",
                 defaults: new { controller = "Blog", action = "Details", id = UrlParameter.Optional, postName = UrlParameter.Optional }
                        );

            // Or this route. It should return /Contact/Index/user-group-2013
            routes.MapRoute(
                 name: "Contact",
                 url: "{pageName}",
                 defaults: new { controller = "Contact", action = "Index", pageName=UrlParameter.Optional }
            );

            // Or this route. It should return /Contact/Index/user-group-2013
            routes.MapRoute(
                 name: "about",
                 url: "{controller}/{action}/{id}/{postName}",
                 defaults: new { controller = "about", action = "about", id = UrlParameter.Optional, postName = UrlParameter.Optional }
                        );



        }
    }
}