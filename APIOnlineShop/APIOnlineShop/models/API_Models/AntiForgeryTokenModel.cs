using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIOnlineShop.models.API_Models
{
    public class AntiForgeryTokenModel
    {
        public string AntiForgeryToken { get; set; }

        public AntiForgeryTokenModel(string antiForgeryToken)
        {
            AntiForgeryToken = antiForgeryToken;
        }

        public AntiForgeryTokenModel()
        {
        }
    }
}