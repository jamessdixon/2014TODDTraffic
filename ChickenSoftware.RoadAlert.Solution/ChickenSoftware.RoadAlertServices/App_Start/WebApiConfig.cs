using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using ChickenSoftware.RoadAlertServices.Models;
using ChickenSoftware.RoadAlertServices.Handlers;

namespace ChickenSoftware.RoadAlertServices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.MessageHandlers.Add(new AuthenticationHandler());
            //config.MessageHandlers.Add(new HeadersHandler());
            
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<TrafficStop>("TrafficStop");
            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );

        }
    }
}
