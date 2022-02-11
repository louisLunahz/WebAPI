using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace APIOnlineShop.Handlers
{
    public class CSRFValidatorHandler: DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string headerToken;
            CookieState cookieToken;
            IEnumerable<string> values;
            if (request.Headers.TryGetValues("skip", out values))
            {
                return base.SendAsync(request, cancellationToken);
            }

            if (!TryRetrieveTokens(request, out headerToken, out cookieToken))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                AntiForgery.Validate(cookieToken.Value, headerToken);
                return base.SendAsync(request, cancellationToken);
            }
            catch (HttpAntiForgeryException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });
        }

        private static bool TryRetrieveTokens(HttpRequestMessage request, out string headerToken, out CookieState cookieToken )
        {
          IEnumerable <string> headerTokens = null;
           headerToken = null;
            cookieToken = null;
           

            if (!request.Headers.TryGetValues("XSRF-TOKEN", out headerTokens) || headerToken.Count() > 1)
            {
                return false;
            }
            headerToken = headerTokens.ElementAt(0);
            cookieToken = request.Headers.GetCookies().Select(cookie => cookie["xsrf-token"]).FirstOrDefault();
            if (cookieToken == null)
            {
                return false;
            }
            return true;
        }
    }
}