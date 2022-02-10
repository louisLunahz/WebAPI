using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace APIOnlineShop.Handlers
{
    //public class OptionsHttpMessageHandler : DelegatingHandler
    //{
    //    protected override Task<HttpResponseMessage> SendAsync(
    //        HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        if (request.Method == HttpMethod.Options)
    //        {
    //            var apiExplorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();

    //            var controllerRequested = request.GetRouteData().Values["controller"] as string;
    //            var supportedMethods = apiExplorer.ApiDescriptions.Where(d =>
    //            {
    //                var controller = d.ActionDescriptor.ControllerDescriptor.ControllerName;
    //                return string.Equals(
    //                    controller, controllerRequested, StringComparison.OrdinalIgnoreCase);
    //            })
    //            .Select(d => d.HttpMethod.Method)
    //            .Distinct();

    //            if (!supportedMethods.Any())
    //                return Task.Factory.StartNew(
    //                    () => request.CreateResponse(HttpStatusCode.NotFound));

    //            return Task.Factory.StartNew(() =>
    //            {
    //                var resp = new HttpResponseMessage(HttpStatusCode.OK);
    //                resp.Headers.Add("Access-Control-Allow-Origin", APIOnlineShop.App_Start.Configs.AllowedDomains);
    //                resp.Headers.Add(
    //                    "Access-Control-Allow-Methods", string.Join(",", supportedMethods));

    //                return resp;
    //            });
    //        }

    //        return base.SendAsync(request, cancellationToken);

    //    }
    //}
}