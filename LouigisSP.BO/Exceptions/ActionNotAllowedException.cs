﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIOnlineShop.BO.Exceptions
{
    public class ActionNotAllowedException : Exception
    {
        public ActionNotAllowedException()
        {
        }

        public ActionNotAllowedException(string message) : base(message)
        {
        }
    }
}