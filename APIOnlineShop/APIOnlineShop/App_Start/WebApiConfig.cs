using APIOnlineShop.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using APIOnlineShop.Exceptions;
using APIOnlineShop.App_Start;

namespace APIOnlineShop
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //var attribute = new System.Web.Http.Cors.EnableCorsAttribute(Configs.AllowedDomains,
              //  "Content-Type, skip, Authorization", "GET, POST, PUT, DELETE, OPTIONS");  //domains, headers, methods - you could do the same for the other args.
            //config.EnableCors(attribute); //global

            // config.MessageHandlers.Add(new APIOnlineShop.Handlers.OptionsHttpMessageHandler());
            config.MessageHandlers.Add(new TokenValidationHandler());

            config.MapHttpAttributeRoutes();

            config.Services.Replace(typeof(IExceptionHandler),new  GlobalExceptionHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
