using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LougisSP.BO.Exceptions
{
    public class ElementNotDeletedException : Exception
    {
        public ElementNotDeletedException()
        {
        }

        public ElementNotDeletedException(string message) : base(message)
        {
        }
    }
}
