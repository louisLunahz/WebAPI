using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace APIOnlineShop.filters
{
    public class ValidateAntiForgeryTokenFilter: System.Web.Http.Filters.ActionFilterAttribute
    {
        private const string XsrfHeader = "XSRF-TOKEN";
        private const string XsrfCookie = "X-XSRF-TOKEN";
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpRequestHeaders headers = actionContext.Request.Headers;
            IEnumerable <string>xsrfTokenList;
            IEnumerable<string> values;
            if (headers.TryGetValues("skip", out values))
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

            CookieState tokenCookie = actionContext.Request.Headers.GetCookies().Select(c =>c[XsrfCookie]).FirstOrDefault();

            if (tokenCookie == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return;
            }

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