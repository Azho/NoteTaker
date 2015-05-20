using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace NoteTakerApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Take this out later if you end up not using it.
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Profile",
                url: "profiles/{action}/{username}",
                defaults: new { controller = "Profiles", action = "Index" }
                );

            routes.MapRoute(
                name: "Search",
                url: "Search",
                defaults: new { controller = "Search", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Notes", action = "Create", id = UrlParameter.Optional }
            );

            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }
}
