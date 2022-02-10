using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIOnlineShop.BO.Exceptions
{
    public class DatabaseInsertionException : Exception
    {
        public DatabaseInsertionException()
        {
        }

        public DatabaseInsertionException(string message) : base(message)
        {
        }
    }
}
