using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LougisSP.BO
{
    public class AntiForgeryTokenModel
    {
        public string AntiForgeryToken { get; set; }

        public AntiForgeryTokenModel(string antiForgeryToken)
        {
            AntiForgeryToken = antiForgeryToken;
        }
    }
}
