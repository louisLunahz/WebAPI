﻿using APIOnlineShop.Exceptions;
using System.Web;
using System.Web.Mvc;

namespace APIOnlineShop
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
