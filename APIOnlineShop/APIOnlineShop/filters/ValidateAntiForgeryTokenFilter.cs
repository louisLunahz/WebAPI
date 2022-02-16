using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Security.Claims;

namespace APIOnlineShop.filters
{
    public class ValidateAntiForgeryTokenFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        private const string XsrfHeader = "X-XSRF-TOKEN";
        private const string XsrfCookie = "XSRF-TOKEN";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpRequestHeaders headers = actionContext.Request.Headers;
            IEnumerable<string> xsrfTokenList;

            var cookies = actionContext.Request.Headers.GetCookies();

            CookieState tokenCookie = actionContext.Request.Headers.GetCookies().Select(c =>
            c[XsrfCookie]).FirstOrDefault();

            if (tokenCookie == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return;
            }


            if (!headers.TryGetValues(XsrfHeader, out xsrfTokenList))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return;
            }

            string tokenHeaderValue = xsrfTokenList.First();
            try
            {
                AntiForgery.Validate(tokenCookie.Value, tokenHeaderValue);
                

            }
            catch (HttpAntiForgeryException)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}