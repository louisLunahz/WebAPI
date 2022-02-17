using LougisSP.BO.api_models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace APIOnlineShop.Controllers
{
    public class AntiforgeryController : ApiController
    {
        [HttpGet]
        [Route("antiforgerytoken")]
        public HttpResponseMessage GetAntiForgeryToken()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            HttpCookie cookie = HttpContext.Current.Request.Cookies["XSRF-TOKEN"];

            string cookieToken;
            string formToken;
            AntiForgery.GetTokens(cookie == null ? "" : cookie.Value, out cookieToken, out formToken);

            AntiForgeryTokenModel content = new AntiForgeryTokenModel
            {
                AntiForgeryToken = formToken
            };

            response.Content = new StringContent(
                     JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

           
            if (!string.IsNullOrEmpty(cookieToken))
            {
                response.Headers.AddCookies(new[]
                {
                    new CookieHeaderValue("XSRF-TOKEN", cookieToken)
                     {
                        Expires = DateTimeOffset.Now.AddMinutes(10),
                        Path = "/; SameSite=None",
                        Secure = true

                     }
                });
            }

            return response;
        }
    }
}
