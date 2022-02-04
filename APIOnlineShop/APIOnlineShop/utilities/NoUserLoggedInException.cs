using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIOnlineShop.utilities
{
    public class NoUserLoggedInException : Exception
    {
        public NoUserLoggedInException()
        {
        }

        public NoUserLoggedInException(string message) : base(message)
        {
        }
    }
}