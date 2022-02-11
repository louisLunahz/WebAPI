using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace APIOnlineShop.Security
{
    public class CSRFTokenGenerator
    {
        public static IEnumerable<string> GetAntiForgeryToken() {
            List<string> tokens = new List<string>();
          //  HttpCookie cookie = HttpContext.Current.Request.Cookies["X-XSRF-TOKEN"];
            string cookieToken;
            string formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            tokens.Add(formToken);
            if (!string.IsNullOrEmpty(cookieToken)) {
                tokens.Add(cookieToken);
            }
            return tokens;
          
        }
    }
}