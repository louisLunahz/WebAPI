using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.SL.Exceptions
{
    public class AddressNotFoundException : Exception
    {
        public AddressNotFoundException()
        {
        }

        public AddressNotFoundException(string message) : base(message)
        {
        }
    }
}
