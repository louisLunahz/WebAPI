﻿using APIOnlineShop.Exceptions;
using APIOnlineShop.filters;
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
