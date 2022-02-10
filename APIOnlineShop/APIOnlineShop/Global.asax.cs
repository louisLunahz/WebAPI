using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using APIOnlineShop.Exceptions;

namespace APIOnlineShop
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new OnExceptionAttribute());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);          

        }

        //protected void Application_BeginRequest()
        //{
        //    string[] allowedOrigin = new string[] { "http://localhost:4200", "http://localhost:2052" };
        //    var origin = HttpContext.Current.Request.Headers["Origin"];
        //    if (origin != null && allowedOrigin.Contains(origin))
        //    {
        //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", origin);
        //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET,POST, DELETE, PUT, OPTIONS");
        //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "content-type");
        //    }
        //}
    }
}
