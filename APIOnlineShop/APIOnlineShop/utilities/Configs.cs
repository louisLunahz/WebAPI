using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace APIOnlineShop.App_Start
{
    public class Configs
    {
        public static string AllowedDomains
        {
            get
            {
                return @ConfigurationManager.AppSettings["AllowedDomains"].ToString();
            }
        }
    }
}