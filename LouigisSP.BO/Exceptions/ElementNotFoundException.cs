using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIOnlineShop.BO.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException()
        {
        }

        public ElementNotFoundException(string message) : base(message)
        {
        }

    }
}
