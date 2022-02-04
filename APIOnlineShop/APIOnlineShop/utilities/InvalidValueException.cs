using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIOnlineShop.utilities
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException()
        {
        }

        public InvalidValueException(string message) : base(message)
        {
        }
    }
}