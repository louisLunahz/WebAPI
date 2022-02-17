using APIOnlineShop.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using APIOnlineShop.Exceptions;
using System.Web.Http.Cors;
using APIOnlineShop.Handlers;

namespace APIOnlineShop
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new JWTTokenValidationHandler());

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
